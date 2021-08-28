using Application.Picking.Interface.OrderPickings;
using Application.Picking.OrderPicking;
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
using Repository.Picking.Interface.Operators;
using Repository.Picking.Interface.OrderPickings;
using Repository.Picking.Operators;
using Repository.Picking.OrderPickings;
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

            // Specific
            services.AddScoped<INextOrderPickingService, Solution.TPSCommon.Picking.Services.NextOrderPickingService>();
            services.AddScoped<IOrderPickingValidator, Solution.TPSCommon.Picking.Business.OrderPickingValidator>();
            services.AddScoped<IOrderPickingEvent, Solution.TPSCommon.Picking.Business.OrderPickingEvent>();

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
        }
    }
}
