using Application.Picking.Interface.OrderPickings;
using Application.Picking.Interface.PickingItems;
using Application.Picking.OrderPicking;
using Application.Picking.PickingItems;
using Business.Domain.Events;
using Business.Domain.Services;
using Business.Domain.Validations;
using Database.Picking;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Picking.Hardware.Handler;
using Picking.Hardware.Handler.Interface;
using Picking.Hardware.Handler.Interface.Message;
using Picking.Hardware.Handler.MQTT;
using Picking.Hardware.Handler.Services;
using Repository.Picking.Interface.Operators;
using Repository.Picking.Interface.OrderPickings;
using Repository.Picking.Interface.PickingItems;
using Repository.Picking.Operators;
using Repository.Picking.OrderPickings;
using Repository.Picking.PickingItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace API.Picking
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

            services.AddControllers()
                .AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API.Picking", Version = "v1" });
            });

            //DbContext
            var connection = Configuration["ConnectionStrings:Picking"];
            services.AddDbContext<PickingDbContext>(options =>
                options.UseMySql(connection, ServerVersion.AutoDetect(connection))
            );

            services.AddHttpClient();


            //Order Picking
            services.AddScoped<IOrderPickingQuery,OrderPickingQuery>();
            services.AddScoped<IOrderPickingApplication, OrderPickingApplication>();
            services.AddScoped<IOperatorRepository>(s => new OperatorRepository("localhost:9859", s.GetService<IHttpClientFactory>()));
            
            services.AddScoped<IOrderPickingProcessApplication, OrderPickingProcessApplication>();
            services.AddScoped<IOrderPickingUpdateRepository, OrderPickingUpdateRepository>();
            services.AddScoped<IPickingItemQuery, PickingItemQuery>();
            services.AddScoped<IPickingItemUpdateRepository, PickingItemUpdateRepository>();
            services.AddScoped<IPickingItemProcessApplication, PickingItemProcessApplication>();

            // Specific
            services.AddScoped<INextOrderPickingService, Solution.TPSCommon.Picking.Services.NextOrderPickingService>();
            services.AddScoped<IOrderPickingValidator, Solution.TPSCommon.Picking.Business.OrderPickingValidator>();
            services.AddScoped<IOrderPickingEvent, Solution.TPSCommon.Picking.Business.OrderPickingEvent>();
            services.AddScoped<IPickingItemEvent, Solution.TPSCommon.Picking.Business.PickingItemEvent>();
            services.AddScoped<IPickingItemValidator, Solution.TPSCommon.Picking.Business.PickingItemValidator>();

            services
               .AddSingleton<IHardwareHandlerManager, HardwareHandlerManager>()
               .AddSingleton<MqttConnection>(new MqttConnection("4530C850-9E43-440E-8ED1-DBEB23599956", "mqtt.eclipseprojects.io", 1883))
               .AddScoped<IPickingFacePostman, PickingFacePostman>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API.Picking v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            new HarwareStartup().Start(app.ApplicationServices);
        }
    }
}
