using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using NLog.Web;
using NLog.Extensions.Logging;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System;
using System.IO;
using Microsoft.Extensions.Hosting;
using CCA.Services.Goober.Config;
using CCA.Services.Goober.Security;
using CCA.Services.Goober.Models;
using CCA.Services.Goober.Service;
using CCA.Services.Goober.DAL;
using CCA.Services.Goober.Tasks;

namespace CCA.Services.Goober
{
    public class Startup
    {
        IConfiguration _configuration;
        public Startup(Microsoft.AspNetCore.Hosting.IHostingEnvironment env)       // ctor
        {
            var builder = new ConfigurationBuilder()        
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            _configuration = builder.Build();                
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .WithMethods("Get", "Post", "Put")
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddMvc(options =>
            {
                options.Filters.Add(new AllowAnonymousFilter());
            }).AddJsonOptions( options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            // task manager  (for background processing)
            // services.AddSingleton<IHostedService, TaskManager>();     

            // Swagger - autodocument setup
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "Goober Service",
                    Version = "v1",
                    Description = "RESTful API, microscervice example, called 'Goober' Service",
                    TermsOfService = "(C) 2018 Epiq  All Rights Reserved."
                });
            });

            string  dbConnectionString = _configuration.GetConnectionString("GooberSvcRepository");

            // DI Dependency injection - built into ASPNETCore 
            services.AddTransient<IResponse, Response>();
            services.AddTransient<HttpClient>();
            services.AddTransient<IJsonConfiguration, JsonConfiguration>();
            services.AddTransient<IRepository>(s => new Repository(dbConnectionString));
            services.AddTransient<IWorker, Worker>();
            services.AddTransient<IRecipe, Recipe>();

            // database context setup
            IJsonConfiguration config = new JsonConfiguration();
            string connection = config.ConnectionString;
            services.AddDbContext<RepositoryContext>(options => options.UseSqlServer(connection));           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Swagger- autodocument
            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Goober Service");
            });

            // JWT/OAuth pipeline
             app.UseMiddleware<AuthenticationMiddleware>(new JsonConfiguration());

            app.UseCors("CorsPolicy");

            app.UseMvc();
            
            // Nlog database log file

            var defaultConnection = _configuration.GetConnectionString("NLogDb");
            NLog.GlobalDiagnosticsContext.Set("defaultConnection", defaultConnection );

            env.ConfigureNLog("nlog.config");
            loggerFactory.AddNLog();

            var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            logger.Info("Goober service started.");


        }
    }
}
