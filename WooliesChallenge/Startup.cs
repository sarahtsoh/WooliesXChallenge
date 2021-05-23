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
using Microsoft.OpenApi.Models;
using WooliesChallenge.CustomMiddleware;
using WooliesChallenge.Models.Configuration;
using WooliesChallenge.Service;
using WooliesChallenge.Service.ApiResources;

namespace WooliesChallenge
{
    public class Startup
    {
        //add cors
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());

            });
            services.Configure<ResourceOptions>(Configuration.GetSection(
                ResourceOptions.resource));
            services.Configure<UserOptions>(Configuration.GetSection(
                UserOptions.user));
            services.AddScoped<IApiService, ApiService>();
            services.AddScoped<ISortMethodFactory, SortMethodFactory>();
            services.AddHttpClient<IApiResource, ApiResource>(c =>
            {
                c.BaseAddress = new Uri(Configuration.GetSection("WooliesBaseURl").Value);
            });
            services.AddScoped<ISortMethodFactory, SortMethodFactory>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Woolies Challenge API",
                    Description = "Woolies Challenge demonstrating action return types"
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
            else
            {
                app.UseMiddleware<ExceptionMiddleware>();
            }
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My ASP.NET Core 3.0 web API v1");
                c.RoutePrefix = string.Empty;
            });

            
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
