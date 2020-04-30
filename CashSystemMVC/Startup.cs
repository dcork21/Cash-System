using CashSystemMVC.Interfaces.Business;
using CashSystemMVC.Interfaces.System;
using CashSystemMVC.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using React.AspNet;
using JavaScriptEngineSwitcher.Extensions.MsDependencyInjection;
using JavaScriptEngineSwitcher.V8;
using Microsoft.EntityFrameworkCore;

namespace CashSystemMVC
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
            var connection = Configuration["DatabaseConnectionString"];
            services.AddDbContext<DataContext>(options => options.UseSqlServer(connection)).AddScoped<DbContext, DataContext>();
                
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Add System interfaces
            services.AddScoped<IUdpClient, UpdClient>();
            services.AddScoped<ICrypto, Crypto>();
            services.AddScoped<IRequestWithdraw, RequestWithdraw>();

            // Add business interfaces
            services.AddScoped<IAccountMgt, AccountMgt>();
            services.AddScoped<IAtmMgt, AtmMgt>();
            services.AddScoped<IBankMgt, BankMgt>();
            services.AddScoped<IIdentityMgt, IdentityMgt>();
            services.AddScoped<IUserMgt, UserMgt>();




            services.AddReact();
            services.AddControllers();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            // Make sure a JS engine is registered, or you will get an error!
            services.AddJsEngineSwitcher(options => options.DefaultEngineName = V8JsEngine.EngineName).AddV8();
            services.AddControllersWithViews();
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
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseReact(config =>
            {
                config
                    .SetLoadBabel(false).SetLoadReact(false)
                    .AddScriptWithoutTransform("~/js/components.js")
                    .AddScriptWithoutTransform("~/js/runtime.js")
                    .AddScriptWithoutTransform("~/js/vendor.js");
            });
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
