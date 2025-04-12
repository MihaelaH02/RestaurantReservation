using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using RestaurantReservation.Common;
using RestaurantReservation.Repository;
using RestaurantReservation.Repository.Users;
using RestaurantReservation.Seeders;
using RestaurantReservation.Services.Users;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using RestaurantReservation.Models.Users;
using RestaurantReservation.Services.EmailSender;
using RestaurantReservation.Repository.System;

namespace RestaurantReservation;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();

        // Прочитане на връзката с базата данни `appsettings.json`
        var connectionString = builder.Configuration.GetConnectionString("OnlineDatabase");

        // Регистрира `DbContext` за работа с SQL Server
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        //`Repository` слой
        builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        builder.Services.AddScoped<AccountRepository>();
        builder.Services.AddScoped<MailRepository>();

        //`Service` слой
        builder.Services.AddScoped<IPasswordHasher<Accounts>, PasswordHasher<Accounts>>();
        builder.Services.AddScoped<JwtService>();
        builder.Services.AddScoped<IMailService, MailSenderService>();
        builder.Services.AddScoped<AccountsService>();

        builder.Services.AddControllersWithViews();

        //JWT конфигурации
        var jwtSettingsSection = builder.Configuration.GetSection("JwtSettings");
        builder.Services.Configure<JwtSettings>(jwtSettingsSection);
        var jwtSettings = jwtSettingsSection.Get<JwtSettings>();
       
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
                };
            });

        builder.Services.AddAuthorization();

        //
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
