using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataStore.EF;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
			if (_env.IsDevelopment())
			{

                //Inject context to DI
                services.AddDbContext<BugsContext>(options =>
                {
                    options.UseInMemoryDatabase("Bugs");
                });
			}

            //2. Add the controller middleware dependency and use the default behavior
            services.AddControllers();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, BugsContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                //Create the in-memory database for dev environment
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated(); 
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                //1. Ues controller middleware
                endpoints.MapControllers();
            });
        }
    }
}
