using Api.Configurations;
using Api.Core.Contracts.Facades;
using Api.Core.Contracts.Repositorys;
using Api.Core.Contracts.Services;
using Api.Core.Contracts.Services.RestServices;
using Api.Core.Facades;
using Api.Core.ModelConfigs;
using Api.Core.Repositorys;
using Api.Core.Services;
using Api.Core.Services.RestServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddTransient<IClienteService, ClienteService>();

            services.Configure<ParametroRestConsultaCEP>(Configuration.GetSection("ParametroConsultaCEP"));
            services.Configure<ParametroRestConsultaEstado>(Configuration.GetSection("ParametroRestConsultaEstado"));
            services.Configure<OrdenacaoEstados>(Configuration.GetSection("OrdenacaoEstados"));
            
            services.AddTransient<IRestClientCEPService, RestClientCEPService>();
            services.AddTransient<IRestClientEstadoService, RestClientEstadoService>();
            services.AddTransient<ICepFacade, CepFacade>();
            services.AddTransient<IEstadoFacade, EstadoFacade>();
            
            services.AddScoped<IClienteRepository, ClienteRepository>();

            services.AddScoped<IConnectionFactoryDatabase>(x =>
            {
                var connectionString = Configuration.GetSection("ConnectionStrings:MySql").Value;
                return new ConnectionFactoryDatabase(connectionString, true);
            });

            services.AddApiVersioning();
            services.AddSwaggerGen(options =>
            {
            //    options.DocInclusionPredicate((docName, apiDesc) =>
            //    {
            //        if (!apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;

            //        var versions = methodInfo.DeclaringType
            //            .GetCustomAttributes(true)
            //            .OfType<ApiVersionAttribute>()
            //            .SelectMany(attr => attr.Versions);

            //        return versions.Any(v => $"v{v.ToString()}" == docName);
            //    });

            //    options.DocumentFilter<TagDescriptionsDocumentFilter>();
                options.OperationFilter<AddInfoToParamVersionOperationFilter>();
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Api Cliente", Description = "Documentação Api Cadastro Básico Cliente", Version = "1.0" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
        }
    }

    public class TagDescriptionsDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Tags = new[] {
            new OpenApiTag { Name = "Livros", Description = "Consulta e mantém os livros." },
            new OpenApiTag { Name = "Listas", Description = "Consulta as listas de leitura." }
        };
        }
    }
}
