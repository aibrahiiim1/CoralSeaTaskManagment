namespace CoralSeaTaskManagment.Ui
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpClient();
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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
