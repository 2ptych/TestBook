using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        void OpenTransaction();
        void Commit();
        void Rollback();
    }
}
