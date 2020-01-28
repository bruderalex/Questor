using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using Questor.Infrasctructure.Data;
using Questor.Web.MIddlewares;

namespace Questor.Web
{
    public class Startup
    {
        public Startup(IHostEnvironment hostEnvironment)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true)
                .AddUserSecrets<Startup>()
                .AddEnvironmentVariables();

            this.Configuration = configurationBuilder.Build();
            this.HostEnvironment = hostEnvironment;
        }

        private IConfiguration Configuration { get; }

        private IHostEnvironment HostEnvironment { get; }

        private ILifetimeScope AutofacContainer { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions()
                .AddSingleton(Configuration);
            services.AddControllersWithViews();

            var sqlConnectionStringBuilder =
                new SqlConnectionStringBuilder(Configuration.GetConnectionString("QuestorDb"));

            if (HostEnvironment.IsDevelopment())
            {
                services.AddDbContext<QuestorContext>(
                    optionsBuilder =>
                        optionsBuilder.UseInMemoryDatabase("QuestorDb"));
            }
            else
            {
                sqlConnectionStringBuilder.Password = Environment.GetEnvironmentVariable("DB_PASS");
                
                services.AddDbContext<QuestorContext>(
                    optionsBuilder =>
                        optionsBuilder.UseSqlServer(sqlConnectionStringBuilder.ConnectionString));
            }

            services.AddMediatR(typeof(QuestorContext).Assembly);
            services.AddAutoMapper(typeof(Startup).Assembly);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterAssemblyModules(typeof(Startup).Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            var context = this.AutofacContainer.Resolve<QuestorContext>();

            if (context.Database.IsSqlServer())
            {
                Policy.Handle<Exception>()
                    .WaitAndRetry(6, retryAttemp => TimeSpan.FromSeconds(Math.Pow(2, retryAttemp)))
                    .Execute(context.Database.Migrate);
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Search/Error");
                //app.UseHsts(); // no https configured for current application
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Search}/{action=Index}/{id?}");
            });
        }
    }
}