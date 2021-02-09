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
using Serilog;
using Serilog.Extensions.Logging;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Serilog.Sinks.Elasticsearch;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            var elasticUrl = Configuration.GetSection("ElasticConfiguration:Url").Value;
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUrl))
                {
                    AutoRegisterTemplate = true,
                })
            .CreateLogger();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddHealthChecks();
            services.AddLogging();
            services.Configure<ParametroRestConsultaCEP>(Configuration.GetSection("ParametroConsultaCEP"));
            services.Configure<ParametroRestConsultaEstado>(Configuration.GetSection("ParametroRestConsultaEstado"));
            services.Configure<OrdenacaoEstados>(Configuration.GetSection("OrdenacaoEstados"));

            services.AddTransient<IRestClientCEPService, RestClientCEPService>();
            services.AddTransient<IRestClientEstadoService, RestClientEstadoService>();
            services.AddTransient<ICepFacade, CepFacade>();
            services.AddTransient<IEstadoFacade, EstadoFacade>();

            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IEnderecoClienteRepository, EnderecoClienteRepository>();

            services.AddTransient<IClienteService, ClienteService>();
            services.AddTransient<IEnderecoClienteService, EnderecoClienteService>();

            services.AddScoped<IConnectionFactoryDatabase>(x =>
            {
                var connectionString = Configuration.GetSection("ConnectionStrings:MySql").Value;
                return new ConnectionFactoryDatabase(connectionString, true);
            });

            services.AddApiVersioning();
            services.AddSwaggerGen(options =>
            {
                options.OperationFilter<AddInfoToParamVersionOperationFilter>();
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Api Cliente", Description = "Documentação Api Cadastro Básico Cliente", Version = "1.0" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            loggerFactory.AddSerilog();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
        }
    }
}
