using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Api.StartupExtensions
{
    public static class FluentValidationExtension
    {
        public static IServiceCollection AddCustomFluentValidation(
            this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.LoadWithPartialName("Application"));

            services.AddControllers()
                .AddFluentValidation(opt =>
                {
                    opt.AutomaticValidationEnabled = true;
                });

            return services;
        }
    }
}
