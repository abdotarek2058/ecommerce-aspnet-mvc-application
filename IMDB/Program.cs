using IMDB.Data;
using IMDB.Data.Cart;
using IMDB.Data.Services;
using IMDB.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IMDB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options=> {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 1;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            //services configuration
            builder.Services.AddScoped<IActorsService,Actorsservice>();
            builder.Services.AddScoped<IProducersService,ProducersService>();
            builder.Services.AddScoped<ICinemasService,CinemasService>();
            builder.Services.AddScoped<IMoviesService,MoviesService>();
            builder.Services.AddScoped<IOrdersService, OrdersService>();

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddMemoryCache();
            builder.Services.AddSession();

            builder.Services.AddScoped<ShoppingCart>(Sp => ShoppingCart.GetShoppingCart(Sp));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            
            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();
           

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Movies}/{action=Index}/{id?}")
                .WithStaticAssets();

            //seed database
            AppDbIntializer.Seed(app);
            AppDbIntializer.SeedUsersAndRolesAsync(app).Wait();
            app.Run();
        }
    }
}
