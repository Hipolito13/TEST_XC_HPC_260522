using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_HPC_260522.Common.Extensions
{
    public static class ServiceExtensions
    {
        #region Application Configuration 
        public static void AddApplication(this IServiceCollection service)
        {
            service.AddRepositories();
            service.AddServices();
        }

        public static void AddAutoMapper(this IServiceCollection service)
        {
            var mapperConfig = new MapperConfiguration(MC =>
            {
                //MC.AddProfile<EntryAutoMapping>();
            });
            IMapper mapper = mapperConfig.CreateMapper();
            service.AddSingleton(mapper);
        }
        #endregion

        #region Swagger Configuration
        public static void AddSwaggerApplication(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Entries  API",
                    Version = "v1",
                    Description = "API para el consumo de servicio Entries-Demo",
                    Contact = new OpenApiContact
                    {
                        Name = "Hipolito Perez Cruz",
                        Email = "hipolitocrz08@gmail.com"
                    },
                });
            });
        }

        public static void AddSwaggerApplication(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "Entries API V1");
            });
        }
        #endregion

        #region Privates Methods
        private static void AddServices(this IServiceCollection service)
        {
            //service.AddScoped<IEntriesServices, EntriesServices>();
        }

        private static void AddRepositories(this IServiceCollection service)
        {
            //service.AddScoped<IUnitOfWork, UnitOfWork>();
        }
        #endregion
    }
}
