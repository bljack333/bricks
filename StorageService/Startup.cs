using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StorageServices.App_Start;
using StorageServices.Services;

namespace StorageService
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
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllHeaders",
                    builder => { builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod().WithExposedHeaders("Access-Control-Allow-Origin"); });
            });
            services.AddMvc();

            services.AddScoped<IStorageRepository, StorageRepository>();
            services.AddScoped<IReferenceRepository, ReferenceRepository>();
            services.AddScoped<ISetRepository, SetRepository>();
            services.AddScoped<IPartsRepository, PartsRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // Seed Data
            }

            app.UseCors("AllowAllHeaders");
            app.UseMvc();

            StorageMapperConfiguration.ConfigureMappings();
        }
    }
}
