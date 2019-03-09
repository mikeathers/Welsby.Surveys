using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Welsby.Surveys.AppSettings;
using Welsby.Surveys.DataLayer.Configurations;

namespace Welsby.Surveys.Api
{
    public class Startup
    {
        private readonly IAppSettings _appSettings;
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IAppSettings appSettings)
        {
            Configuration = configuration;
            _appSettings = appSettings;
            var config = _appSettings.GetConfiguration();
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(config).CreateLogger();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var configuration = _appSettings.GetConfiguration();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddLogging();
            
            services.AddDbContext<SurveyDbContext>(options => options.UseSqlServer(connectionString,
                b => b.MigrationsAssembly("Welsby.Surveys.DataLayer")));

            services.AddAutoMapper();

           

            // Add AutoFac
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<ServiceLayer.Utils.AutoFacModule>();
            containerBuilder.Populate(services);
            var container = containerBuilder.Build();
            return new AutofacServiceProvider(container);

            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            loggerFactory.AddSerilog();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
