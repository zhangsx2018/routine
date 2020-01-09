using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using routine.Api.Services;
using routine.Api.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace routine.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void  ConfigureServices(IServiceCollection services)
        {
           
            services.AddControllers();

            services.AddScoped ( typeof(Repository<,>));
            // services.AddScoped<IRepository,Repository>();
            services.AddDbContext<RoutineDbContext>(op =>
            {
                op.UseSqlite("Data Source = routine.db");
            });
            /*
             *×¢²áÊý¾Ý¿â
             */
            services.AddDbContext<DeviceDbContext>(op =>
            {
                op.UseMySql(Configuration.GetConnectionString("DefaultConnection"));
            });
            /*
             * ×¢²ánacos
             */
            services.AddNacosAspNetCore(Configuration);
            /*
             * ×¢²áautoMapper
             */
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
           
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

      
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder => 
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("Unexpected Error");
                    });
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseNacosAspNetCore();
        }
    }
}
