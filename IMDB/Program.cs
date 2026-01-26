using IMDB.Data;
using IMDB.Data.Services;
using Microsoft.EntityFrameworkCore;

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

            //services configuration
            builder.Services.AddScoped<IActorsService,Actorsservice>();
            builder.Services.AddScoped<IProducersService,ProducersService>();
            builder.Services.AddScoped<ICinemasService,CinemasService>();
            builder.Services.AddScoped<IMoviesService,MoviesService>();

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

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Movies}/{action=Index}/{id?}")
                .WithStaticAssets();

            //seed database
            AppDbIntializer.Seed(app);
            app.Run();
        }
    }
}
