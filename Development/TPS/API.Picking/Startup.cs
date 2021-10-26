using Application.Picking.Interface.OrderPickings;
using Application.Picking.Interface.PickingItems;
using Application.Picking.OrderPicking;
using Application.Picking.PickingItems;
using Business.Domain.Events;
using Business.Domain.Services;
using Business.Domain.Validations;
using Database.Picking;
using Infrastructure.MQTT;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Repository.Picking.Interface.Operators;
using Repository.Picking.Interface.OrderPickings;
using Repository.Picking.Interface.PickingItems;
using Repository.Picking.Operators;
using Repository.Picking.OrderPickings;
using Repository.Picking.PickingItems;
using Service.PickToLight;
using Service.PickToLight.Interface;
using Service.PickToLight.Interface.Warehouse;
using Service.PickToLight.Picking;
using Service.PickToLight.Warehouse;
using System;
using System.Net.Http;

namespace API.Picking
{
    public class Startup
    {
        private string corsPolicyName = "Unique";
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {

            services.AddControllers()
                .AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API.Picking", Version = "v1" });
            });
            services.AddCors(options =>
                options.AddPolicy(
                    corsPolicyName,
                    policy =>
                        policy.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                )
            );

            //DbContext
            var connection = Configuration["ConnectionStrings:Picking"];
            services.AddDbContext<PickingDbContext>(options =>
                options.UseMySql(connection, ServerVersion.AutoDetect(connection))
            );

            services.AddHttpClient();


            //Order Picking
            services.AddScoped<IOrderPickingQuery, OrderPickingQuery>();
            services.AddScoped<IOrderPickingApplication, OrderPickingApplication>();
            services.AddScoped<IOperatorRepository>(s => new OperatorRepository(Configuration.GetSection("AppSettings:AdministrationApi").Value, s.GetService<IHttpClientFactory>()));

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


            services.AddSingleton<MQTTConnection>(
                    new MQTTConnection(Configuration.GetSection("AppSettings:MqttClient").Value
                    , Configuration.GetSection("AppSettings:MqttServer").Value
                    , Int32.Parse(Configuration.GetSection("AppSettings:MqttPort").Value), true, true));

            services.AddScoped<IPickingFaceService, PickingFaceService>();

            services.AddScoped<IItemStockProxyRepository>(x => 
                new ItemStockProxyRepository(Configuration.GetSection("AppSettings:WarehouseApi").Value
                    , x.GetService<IHttpClientFactory>()));

            services.AddSingleton<IConfirmListenerService, ConfirmListenerService>()
                .AddSingleton<IOrderPickingProxyRepository>(x =>
                    new OrderPickingProxyRepository(
                        x.GetService<IHttpClientFactory>()
                        , Configuration.GetSection("AppSettings:PickingApi").Value))
                .AddSingleton<IPickingItemProcessProxyRepository>(x =>
                    new PickingItemProcessProxyRepository(
                        x.GetService<IHttpClientFactory>()
                        , Configuration.GetSection("AppSettings:PickingApi").Value));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API.Picking v1"));
            }

            app.UseRouting();

            app.UseCors(corsPolicyName);

            app.UseAuthorization();


            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });


            app.ApplicationServices.GetService<IConfirmListenerService>().Setup();
            //new HarwareStartup().Start(app.ApplicationServices);
        }
    }
}
