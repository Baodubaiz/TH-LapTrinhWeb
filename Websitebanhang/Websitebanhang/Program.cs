using Microsoft.EntityFrameworkCore;
using Websitebanhang.Models;
using Websitebanhang.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Websitebanhang
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Cấu hình DbContext
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Cấu hình Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddDefaultUI();

            // Cấu hình Cookie cho Authentication
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Identity/Account/Login";
                options.LogoutPath = "/Identity/Account/Logout";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";  // Sửa lỗi lặp LogoutPath
            });

            // Đăng ký Repository
            builder.Services.AddScoped<IProductRepository, EFProductRepository>();
            builder.Services.AddScoped<ICategoryRepository, EFCategoryRepository>();

            // Thêm Razor Pages và MVC
            builder.Services.AddRazorPages();
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Xử lý lỗi trong môi trường Production
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            // Cấu hình Endpoint cho Razor Pages
            app.MapRazorPages();

            // Cấu hình Routing cho Controller
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Admin",
                    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.Run();
        }
    }
}
