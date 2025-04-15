using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Models.AdditionalFunctionality;
using RestaurantReservation.Models.Nomenclatures;
using RestaurantReservation.Models.Other;
using RestaurantReservation.Models.Reservation;
using RestaurantReservation.Models.RestaurantsSettings;
using RestaurantReservation.Models.System;
using RestaurantReservation.Models.Users;
using System.Diagnostics.Metrics;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    /// <summary> Tаблици за допълнителни функционалности </summary>
    public DbSet<Comments>                      Comments { get; set; }
    public DbSet<RestaurantSearchFilter>        RestaurantSearchFilter { get; set; }

    /// <summary> Таблици с номенклатури </summary>
    public DbSet<AccountRoleType>               AccountRoleTypes { get; set; }
    public DbSet<NotificationEvents>            NotificationEvents { get; set; }
    public DbSet<RestaurantAtmosphere>          RestaurantAtmospheres { get; set; }
    public DbSet<RestaurantLocations>           RestaurantLocations { get; set; }
    public DbSet<RestaurantSpecialConditions>   RestaurantSpecialConditions { get; set; }
    public DbSet<RestaurantTableTypes>          RestaurantTableTypes { get; set; }

    /// <summary> Таблици за резервации </summary>
    public DbSet<ReservationRequestQueue>       ReservationRequestQueues { get; set; }
    public DbSet<Reservations>                  Reservations { get; set; }
    public DbSet<RestaurantMonthlySchedule>     RestaurantMonthlySchedules { get; set; }

    /// <summary> Таблици с настойки за ресторанти </summary>
    public DbSet<RestaurantCapacitySheme>       RestaurantCapacityShemes { get; set; }
    public DbSet<RestaurantImages>              RestaurantImages { get; set; }
    public DbSet<RestaurantScheduleSettings>    RestaurantScheduleSettings { get; set; }

    /// <summary> Системни таблици </summary>
    public DbSet<NotificationSettings>          NotificationSettings { get; set; }

    /// <summary> Таблици за потребители </summary>
    public DbSet<Accounts>                      Accounts { get; set; }
    public DbSet<Clients>                       Clients { get; set; }
    public DbSet<Restaurants>                   Restaurants { get; set; }
    public DbSet<PasswordResetToken>            PasswordResetTokens { get; set; }
}
