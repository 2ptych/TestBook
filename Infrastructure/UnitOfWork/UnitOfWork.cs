using Infrastructure.Context;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IDbContextTransaction _transaction;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public void OpenTransaction()
        {
            if (_transaction == null)
                _transaction = _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                _context.SaveChanges();
                _transaction.Commit();
                _transaction = null;
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                this._transaction.Dispose();
                this._transaction = null;
            }

            _context.Dispose(); // ?
        }

        public void Rollback()
        {
            if (_transaction != null)
                _transaction.Rollback();
        }
    }
}
