using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarDealershipInventory.Core.ApplicationServices;
using CarDealershipInventory.Core.ApplicationServices.Impl;
using CarDealershipInventory.Core.ApplicationServices.Validators;
using CarDealershipInventory.Core.ApplicationServices.Validators.Interfaces;
using CarDealershipInventory.Core.DomainServices;
using CarDealershipInventory.Infrastructure.Data;
using CarDealershipInventory.Infrastructure.Data.Repositories;
using CarDealershipInventory.Infrastructure.Data.Security;
using CarDealershipInventory.Infrastructure.DataInitialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace CarDealershipInventory.UI.RestAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Byte[] secretBytes = new byte[40];
            Random rand = new Random();
            rand.NextBytes(secretBytes);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretBytes),
                    ValidateLifetime = true, //validate the expiration and not before values in the token
                    ClockSkew = TimeSpan.FromMinutes(5) //5 minute tolerance for the expiration date
                };
            });

            services.AddCors(options =>
            {
                options.AddPolicy(name: "CarDealershipDev",
                   builder =>
                   {
                       builder.WithOrigins("http://localhost:4200", "https://localhost:4200")
                       .AllowAnyMethod().AllowAnyHeader();
                   });
                options.AddPolicy(name: "CarDealershipProd",
                   builder =>
                   {
                       builder.WithOrigins("http://cardealershipinventorywebapp.azurewebsites.net", "https://cardealershipinventorywebapp.azurewebsites.net")
                       .AllowAnyMethod().AllowAnyHeader();
                   });
            }
               
            );

            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });

            if(Environment.IsDevelopment())
            {
                services.AddDbContext<CarDealershipInventoryContext>(
                opt =>
                {
                    opt.UseLoggerFactory(loggerFactory)
                    .UseSqlite("Data Source=cardealershipinventory.db");                    
                }, ServiceLifetime.Transient
                );
                services.AddTransient<IDataInitializer, SqlLiteInitializer>();
            }
            else
            {
                services.AddDbContext<CarDealershipInventoryContext>(
                opt =>
                {
                    opt.UseSqlServer(Configuration.GetConnectionString("defaultConnection"));                    
                }
                );
                services.AddTransient<IDataInitializer, SqlServerInitializer>();
            }

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IModelRepository, ModelRepository>();
            services.AddScoped<IModelService, ModelService>();
            services.AddScoped<IModelValidator, ModelValidator>();
            services.AddScoped<IManufacturerValidator, ManufacturerValidator>();
            services.AddScoped<ICarValidator, CarValidator>();
            services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
            services.AddScoped<IManufacturerService, ManufacturerService>();

            services.AddSingleton<IAuthenticationHelper>(new
                AuthenticationHelper(secretBytes));

            services.AddControllers();

            services.AddMvc().AddNewtonsoftJson(options => {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //options.SerializerSettings.MaxDepth = 1;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseCors("CarDealershipDev");
            }
            else
            {
                app.UseCors("CarDealershipProd");
            }

            //app.UseDeveloperExceptionPage();
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var ctx = scope.ServiceProvider.GetService<CarDealershipInventoryContext>();
                var db = scope.ServiceProvider.GetService<IDataInitializer>();
                db.Initialize(ctx);
            }

            app.UseHttpsRedirection();

            app.UseRouting();            

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
