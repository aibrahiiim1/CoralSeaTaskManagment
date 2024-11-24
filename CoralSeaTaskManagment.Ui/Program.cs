using CoralSeaTaskManagment.Model.Models.Domain;
using CoralSeaTaskManagment.Security;
using CoralSeaTaskManagment.Services;
using CoralSeaTaskManagment.Ui.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;
namespace CoralSeaTaskManagment.Ui
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddRazorPages()
                .AddRazorRuntimeCompilation();
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddDistributedMemoryCache(); 
            builder.Services.AddSession(options => { options.IdleTimeout = TimeSpan.FromMinutes(60); options.Cookie.HttpOnly = true; options.Cookie.IsEssential = true; });
            //builder.Services.AddServerSideBlazor();
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //builder.Services.AddScoped<ProtectedLocalStorage>();

            builder.Services.AddScoped<CookieService>();
            builder.Services.AddScoped<AccessTokenServies>();
            builder.Services.AddScoped<AuthService>();
            builder.Services.AddScoped<APIService>();
            builder.Services.AddScoped<RefreshTokenService>();
            builder.Services.AddScoped<ResourceService>();
            builder.Services.AddHttpClient("ApiClient", opt =>
            {
                opt.BaseAddress = new Uri(ApiRequests.BasicUrl);
            });
            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication().AddScheme<CustOption, JWTAuthenticationHandler>(
                "JWTAuth", opt =>
                {

                });

            builder.Services.AddScoped<JWTAuthenticationStateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider, JWTAuthenticationStateProvider>();
            builder.Services.AddCascadingAuthenticationState();
            var app = builder.Build();

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjA1NkAzMjM2MkUzMTJFMzliSzVTQlJKN0NLVzNVOFVKSlErcVEzYW9PSkZ2dUhicHliVjkrMncxdHpRPQ==");
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();

            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapBlazorHub(); 
                //endpoints.MapFallbackToPage("/_Host"); 
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Order}/{action=Index}/{id?}"); });
            //app.MapControllerRoute(

            //    name: "default",
            //    pattern: "{controller=Order}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
