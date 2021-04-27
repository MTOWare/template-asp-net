using Template.Core.CustomEntities;
using Template.Core.Interfaces;
using Template.Core.Services;
using Template.Infraestructure.Data;
using Template.Infraestructure.Interfaces;
using Template.Infraestructure.Repositories;
using Template.Infraestructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using Template.Infrastructure.Interfaces;
using Template.Infrastructure.Services;
using Template.Infrastructure.Options;

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
            
            services.AddTransient<IUserService, UserService>();
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ISendGridService, SendGridService>();

            services.AddTransient<ISecurityService, SecurityService>();
            services.AddSingleton<IPasswordService, PasswordService>();


            services.AddSingleton<IUriService>(provider =>
            {
                var accesor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accesor.HttpContext.Request;
                var absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(absoluteUri);
            });
         
            return services;
        }

        public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<PaginationOptions>(options => configuration.GetSection("Pagination").Bind(options));

            services.Configure<PasswordOptions>(options => configuration.GetSection("PasswordOptions").Bind(options));

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
