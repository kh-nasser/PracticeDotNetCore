using eshop_webapi.Contracts;
using eshop_webapi.Models;
using eshop_webapi.Repositories;
using EshopApi.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop_webapi
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
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "swagger doc"
                });
                swagger.IncludeXmlComments(Path.Combine(Directory.GetCurrentDirectory(), @"bin\Debug\netcoreapp3.1", "eshop webapi.xml"));
            });
            services.AddControllers();

            services.AddDbContext<EshopApi_DBContext>(options =>
            {
                options.UseSqlServer("Data Source=.; Initial Catalog=EshopApi_DB;Integrated Security=True;");
            });

            //define <ICustomerRepository> and <CustomerRepository> dependency
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ISalesPersonsRepository, SalesPersonsRepository>();

            //enable data-caching
            services.AddResponseCaching();
            //enable memory-cache
            services.AddMemoryCache();

            //JWT
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuer = true,//validate token over server
                            ValidateAudience = false, //validate token over client
                            ValidateLifetime = true, //key will expire
                            ValidateIssuerSigningKey = true,//validate token, key , tokne
                            ValidIssuer = "http://localhost:3962", //server uri: valid auth server 
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authnetication"))//encription key
                        };
                });
            //IdentityModelEventSource.ShowPII = true;//remove in deploy

            //allow other application to use auth
            services.AddCors(options =>
            {
                options.AddPolicy("EnableCors", builder =>
                {
                    builder
                    .SetIsOriginAllowed(origin => true) // allow any origin
                                                        //.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .Build();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "swager");
            });

           
            //define middleware
            app.UseCors("EnableCors");
            //app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //user data-caching
            app.UseResponseCaching();
        }
    }
}
