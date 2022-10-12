using FreeCourse.Web.Services.Interfaces;
using FreeCourse.Web.Services;
using Microsoft.Extensions.DependencyInjection;
using FreeCourse.Web.Handler;
using FreeCourse.Web.Models;
using System;
using Microsoft.Extensions.Configuration;
using FreeCourse.Shared.Services;
using FreeCourse.Web.Helpers;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace FreeCourse.Web.Extensions
{
    public static class ServiceExtension
    {

        public static void AddHttpClientServices(this IServiceCollection services, IConfiguration Configuration)
        {
            var serviceApiSettings = Configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();

            services.AddHttpClient<IOrderService, OrderService>(i =>
            {
                i.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.Order.Path}");
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

            services.AddHttpClient<IPaymentService, PaymentService>(i =>
            {
                i.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.Payment.Path}");
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

            services.AddHttpClient<IPhotoStockService, PhotoStockService>();
            services.AddHttpClient<ICatalogService, CatalogService>(i =>
            {
                i.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.Catalog.Path}");
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

            services.AddHttpClient<IDiscountService, DiscountService>(i =>
            {
                i.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.Discount.Path}");
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

            services.AddHttpClient<IClientCredentialTokenService, ClientCredentialTokenService>();
            services.AddHttpClient<IIdentityService, IdentityService>();

            services.AddHttpClient<IUserService, UserService>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.Basket.Path}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddHttpClient<IBasketService, BasketService>(opt =>
            {
                opt.BaseAddress = new Uri(serviceApiSettings.IdentityBaseUrl);
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddSingleton<PhotoHelper>();

            services.AddScoped<ResourceOwnerPasswordTokenHandler>();

            services.AddScoped<ClientCredentialTokenHandler>();

            services.AddAccessTokenManagement();

            services.AddScoped<ISharedIdentityService, SharedIdentityService>();

            services.Configure<ServiceApiSettings>(Configuration.GetSection("ServiceApiSettings"));

            services.Configure<ClientSettings>(Configuration.GetSection("ClientSettings"));

            services.AddHttpContextAccessor();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
                CookieAuthenticationDefaults.AuthenticationScheme,
                opt =>
                {
                    opt.LoginPath = "Auth/SignIn";
                    opt.ExpireTimeSpan = TimeSpan.FromDays(60);
                    opt.SlidingExpiration = true;
                    opt.Cookie.Name = "udemywebcookie";
                });
        }
    }
}
