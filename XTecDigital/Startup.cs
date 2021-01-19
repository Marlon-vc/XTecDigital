using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XTecDigital.Models;
using AutoMapper;
using XTecDigital.Helpers;
using System.IO;
using System;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http;

namespace XTecDigital
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
            services.AddCors();

            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddDbContext<AppDbContext>(options => 
            {
                options.UseSqlServer("Server=tcp:xtecdigitalcr.database.windows.net,1433;Initial Catalog=xtecdigital2;Persist Security Info=False;User ID=xtec_admin;Password=ONCEdeENERO-99;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            });

            services.AddAutoMapper(options =>
            {
                options.AddProfile<XTecDigitalProfile>();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseCors(options => options.WithOrigins("*").AllowAnyMethod().AllowAnyHeader());

            // Configuración de la carpeta de documentos
            var storagePath = Path.Combine(Environment.CurrentDirectory, "Storage");
            
            //Configurar storage path
            FileHandler.StoragePath = storagePath;

            if (!Directory.Exists(storagePath))
            {
                var dir = Directory.CreateDirectory(storagePath);
            }

            if (!Directory.Exists(storagePath))
            {
                throw new InvalidOperationException("El directorio de almacenamiento no se pudo crear");
            }
            app.UseStaticFiles(new StaticFileOptions {
                FileProvider = new PhysicalFileProvider(storagePath),
                RequestPath = new PathString("/storage")
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
