using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using Template.Core.CustomEntities;
using Template.Core.Interfaces;
using Template.Infraestructure.Data;
using Template.Infraestructure.Repositories;

namespace Template.Infraestructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<TemplateContext>(options =>
              options.UseNpgsql(configuration.GetConnectionString("{{Database_Name}}Connection"), options =>
              {
                  options.SetPostgresVersion(new Version(9, 5));
              })
           );

            return services;
        }
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();

         
            return services;
        }

        public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<PaginationOptions>(options => configuration.GetSection("Pagination").Bind(options));

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Template.Api", Version = "v1" });
            });

            return services;
        }

    }
}
