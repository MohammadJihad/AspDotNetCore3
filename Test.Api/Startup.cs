using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using Test.Api.Models;
using Test.Business;
namespace Test.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(option => option.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Latest);
            services.AddSession();
            services.AddCors();
            services.AddAutoMapper(new Type[] { typeof(AppMapping) });
            services.AddControllers();

            services.AddOpenApiDocument();
            // configure DI for application services
            services.RegisterServices(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.DataSeed();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(b => b.WithOrigins(Configuration["Appsettings:ClientURL"]).AllowAnyHeader().AllowAnyMethod());
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                RequestPath = new PathString("/Resources")
            });

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseSession();

            app.UseMvc();

            app.UseOpenApi();
            app.UseSwaggerUi3();
        }
    }
}
