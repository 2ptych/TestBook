using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.StartupExtensions
{
    public static class JwtAuthExtension
    {
        public static IServiceCollection AddCustomJwtAuthentication(
            this IServiceCollection services, IConfiguration Configuration)
        {
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(/*"Common", */options =>
                {
                    options.TokenValidationParameters =
                        new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer =
                                Configuration["JwtOptions:ValidIssuer"],

                            ValidateAudience = true,
                            ValidAudience =
                                Configuration["JwtOptions:ValidAudience"],

                            ValidateLifetime = true,
                            
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(
                                    Configuration["JwtOptions:SigningKey"]))
                        };
                });

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    //.AddAuthenticationSchemes("Common")
                    .Build();

                options.AddPolicy("admin",
                    policy => policy.RequireRole("admin"));
                options.AddPolicy("user",
                    policy => policy.RequireRole("user"));
            });

            return services;
        }

        public static IApplicationBuilder UseCustomJwtAuthentication(
            this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }
    }
}
