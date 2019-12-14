using AutoMapper;
using Customers.Application;
using Customers.Application.Contracts;
using Customers.Application.Queries;
using Customers.Application.Services;
using Customers.Infrastructure.Auth;
using Customers.Infrastructure.NHibernate;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Customers.API
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
            services.AddSingleton(new SessionFactory(Environment.GetEnvironmentVariable("MYSQL_STRCON_CORE_CUSTOMERS")));
            services.AddSingleton(new Hasher());
            services.AddControllers();
            services.AddScoped<IAuthApplicationService, AuthApplicationService>();
            services.AddScoped<ICustomerApplicationService, CustomerApplicationService>();            
            services.AddSingleton<ICustomerQueries, CustomerMySQLDapperQueries>();
            services.AddCors(o => o.AddPolicy("AllowOrigin", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseCors("AllowOrigin");
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
                endpoints.MapControllers()
            );
        }
    }
}
