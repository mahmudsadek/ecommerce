using ecommerce.Models;
using ecommerce.Repository;
using ecommerce.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ecommerce
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //inject the context
            builder.Services.AddDbContext<Context>(
                options => {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("cs"));
                });

            //register Model.
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

            // omar : registering order repo
            builder.Services.AddScoped< IOrderRepository , OrderRepository >();


            // omar : registering orderservice
            builder.Services.AddScoped<IOrderService, OrderService>();


            //register the identityuser
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
                options => { options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequireUppercase = true;
                }
                ).AddEntityFrameworkStores<Context>();

            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
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

