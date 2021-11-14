using LandingRocket.BusinessLogic;
using LandingRocket.BusinessLogic.DataStorage;
using LandingRocket.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandingRocket.API
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
            services.AddScoped<ILandingRocketService, LandingRocketService>();

            BusinessLogic.DataStorage.GlobalData.landingArea_X = int.Parse(Configuration["landingArea_X"]);
            BusinessLogic.DataStorage.GlobalData.landingArea_Y = int.Parse(Configuration["landingArea_Y"]);
            BusinessLogic.DataStorage.GlobalData.landingPlatform_X = int.Parse(Configuration["landingPlatform_X"]);
            BusinessLogic.DataStorage.GlobalData.landingPlatform_Y = int.Parse(Configuration["landingPlatform_Y"]);
            BusinessLogic.DataStorage.GlobalData.startingPosition_X = int.Parse(Configuration["startingPosition_X"]);
            BusinessLogic.DataStorage.GlobalData.startingPosition_Y = int.Parse(Configuration["startingPosition_Y"]);
            BusinessLogic.DataStorage.GlobalData._landingArea = new int[BusinessLogic.DataStorage.GlobalData.landingArea_X, BusinessLogic.DataStorage.GlobalData.landingArea_Y];
            BusinessLogic.DataStorage.GlobalData.SetInitialValues();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Landing Rocket", Version = "1.0", });

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger(c =>
                c.PreSerializeFilters.Add((swagger, httpReq) =>
                {
                   //Clear servers -element in swagger.json because it got the wrong port when hosted behind reverse proxy
                   swagger.Servers.Clear();
                }));

                app.UseSwaggerUI(c =>
                {

                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Landing Rocket V1");
                    c.RoutePrefix = string.Empty;
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
