using Microsoft.EntityFrameworkCore;
using CRUD_OPERATION.Models;

namespace CRUD_OPERATION
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<CRUD_OPERATION.Models.AppContext>(item =>
            item.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))

            );
            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            //for logout
            builder.Services.AddAuthentication("Cookies") // "Cookies" is the default scheme name
  .AddCookie("Cookies", options =>
  {
      options.LoginPath = "/Login/Create"; // Set your login path
      options.LogoutPath = "/Login/Logout"; // Set your logout path
      options.AccessDeniedPath = "/Login/AccessDenied"; // Set your access denied path
  });

            //for logout
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSession();

            //for logout
            app.Use(async (context, next) =>
            {
                context.Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate"; // HTTP 1.1.
                context.Response.Headers["Pragma"] = "no-cache"; // For HTTP 1.0.
                context.Response.Headers["Expires"] = "0"; // Expire immediately
                await next();
            });

            //for logout
            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
