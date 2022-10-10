using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreeCourse.Web.Handler;
using FreeCourse.Web.Models;
using FreeCourse.Web.Services;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace FreeCourse.Web
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
            services.AddScoped<ResourceOwnerPasswordTokenHandler>();

            services.Configure<ServiceApiSettings>(Configuration.GetSection("ServiceApiSettings"));

            services.Configure<ClientSettings>(Configuration.GetSection("ClientSettings"));

            var serviceApiSettings = Configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();

            services.AddHttpContextAccessor();
            services.AddHttpClient<IIdentityService, IdentityService>();

            services.AddHttpClient<IUserService, UserService>(opt =>
            {
                opt.BaseAddress = new Uri(serviceApiSettings.IdentityBaseUrl);
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
                CookieAuthenticationDefaults.AuthenticationScheme,
                opt =>
                {
                    opt.LoginPath = "Auth/SignIn";
                    opt.ExpireTimeSpan = TimeSpan.FromDays(60);
                    opt.SlidingExpiration = true;
                    opt.Cookie.Name = "udemywebcookie";
                });

            

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
            }
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
