using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Seeders;

namespace RestaurantReservation;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();

        // Прочитане на връзката с базата данни appsettings.json
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        // Регистрира `DbContext` за работа с SQL Server
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Изпълнение на Seeder
        using (var serviceScope = app.Services.CreateScope())
        {
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            new ApplicationDbContextSeeder().SeedAsync(dbContext).GetAwaiter().GetResult();
        }

        app.MapDefaultEndpoints();

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
