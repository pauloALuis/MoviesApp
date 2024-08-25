using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration   configuration){

            services.AddControllers(
            // options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true

            );
            services.AddDbContext<DataContext>(opts =>
                    {
                        opts.UseSqlite(configuration.
                        GetConnectionString("DefaultConnection"));
                    });

           services.AddCors();
           services.AddScoped<ITokenService, TokenServices>();

           return services;

        }
    }
}