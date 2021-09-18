using Database.Warehouse;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Repository.Warehouse.Interface.ItemStocks;
using Repository.Warehouse.Interface.Sectors;
using Repository.Warehouse.ItemStocks;
using Repository.Warehouse.Sectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Warehouse
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API.Warehouse", Version = "v1" });
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
            var connection = Configuration["ConnectionStrings:Warehouse"];
            services.AddDbContext<WarehouseDbContext>(options =>
                options.UseMySql(connection, ServerVersion.AutoDetect(connection))
            );

            //Sector
            services.AddScoped<ISectorRepository, SectorRepository>();
            services.AddScoped<IItemStockRepository, ItemStockRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API.Warehouse v1"));
            }

            app.UseRouting();

            app.UseCors(corsPolicyName);

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });


            app.UseCors(corsPolicyName);
        }
    }
}
