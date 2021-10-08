using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataStore.EF;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WebApi.Auth;

namespace PlatformDemo
{
    public class Startup
    {
		private readonly IWebHostEnvironment _env;

		public Startup(IWebHostEnvironment env)
		{
            _env = env;
		}
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ICustomTokenManager, JwtTokenManager>();
            services.AddSingleton<ICustomUserManager, CustomUserManager>();

			if (_env.IsDevelopment())
			{
                //Inject context to DI
                services.AddDbContext<BugsContext>(options =>
                {
                    options.UseInMemoryDatabase("Bugs");
                });
			}

            services.AddControllers();

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                //Using header to determine api version
                //options.ApiVersionReader = new HeaderApiVersionReader("X-API-Version");
                //Test: X-API-Version 1.0/2.0

                //Using query string to determine api version
                //https://localhost:44314/api/tickets?api-version=2.0
            });

            services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");

            services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "My Web API v1", Version = "version 1" });
                options.SwaggerDoc("v2", new OpenApiInfo { Title = "My Web API v2", Version = "version 2" });
            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("https://localhost:44311")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, BugsContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                //Create the in-memory database for dev environment
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                //Configure OpenAPI
                app.UseSwagger();
                app.UseSwaggerUI(
                    options => {
                        options.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1");
                        options.SwaggerEndpoint("/swagger/v2/swagger.json", "WebAPI v2");
                    });
            }

            app.UseRouting();

            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
