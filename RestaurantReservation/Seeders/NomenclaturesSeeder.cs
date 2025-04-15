using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Models.Nomenclatures;

namespace RestaurantReservation.Seeders
{
    public class NomenclaturesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext context)
        {

            if (!context.AccountRoleTypes.Any())
            {
                await context.AccountRoleTypes.AddRangeAsync(
                    new AccountRoleType { Role = "Admin" },
                    new AccountRoleType { Role = "Restaurant" },
                    new AccountRoleType { Role = "Client" }
                );
            }

            if (!context.NotificationEvents.Any())
            {
                await context.NotificationEvents.AddRangeAsync(
                     new NotificationEvents { Event = "Регистрация" },
                     new NotificationEvents { Event = "Смяна на парола" },
                     new NotificationEvents { Event = "Блокиран потребител" },
                     new NotificationEvents { Event = "Заявена резервация" },
                     new NotificationEvents { Event = "Потвърдена резервация" },
                     new NotificationEvents { Event = "Отказана резервация" },
                     new NotificationEvents { Event = "Призив за обратна връзка" },
                     new NotificationEvents { Event = "Отнети точки" },
                     new NotificationEvents { Event = "Информация" },
                     new NotificationEvents { Event = "Друго" }
                );
            }

            if (!context.RestaurantAtmospheres.Any() )
            {
                await context.RestaurantAtmospheres.AddRangeAsync(
                    new RestaurantAtmosphere { Atmosphere = "Ресторант" },
                    new RestaurantAtmosphere { Atmosphere = "Бар" },
                    new RestaurantAtmosphere { Atmosphere = "Италиански" },
                    new RestaurantAtmosphere { Atmosphere = "Мексикански" },
                    new RestaurantAtmosphere { Atmosphere = "Китайски" },
                    new RestaurantAtmosphere { Atmosphere = "Азиатски" },
                    new RestaurantAtmosphere { Atmosphere = "Гръцки" },
                    new RestaurantAtmosphere { Atmosphere = "Турски" }

                );
            }

            if (!context.RestaurantLocations.Any())
            {
                await context.RestaurantLocations.AddRangeAsync(
                    new RestaurantLocations { Location = "Първи етаж" },
                    new RestaurantLocations { Location = "Втори етаж" },
                    new RestaurantLocations { Location = "Трети етаж" },
                    new RestaurantLocations { Location = "Друг етаж" },
                    new RestaurantLocations { Location = "Градина" },
                    new RestaurantLocations { Location = "Тераса" },
                    new RestaurantLocations { Location = "Зала за пушачи" },
                    new RestaurantLocations { Location = "Зала за непушачи" },
                    new RestaurantLocations { Location = "Шатра" },
                    new RestaurantLocations { Location = "Специална зала" },
                    new RestaurantLocations { Location = "Друго" }
                );
            }

            if (!context.RestaurantSpecialConditions.Any())
            {
                await context.RestaurantSpecialConditions.AddRangeAsync(
                    new RestaurantSpecialConditions { Status = new byte[] { 0x00 }, SpecialCondition = "Няма" },
                    new RestaurantSpecialConditions { Status = new byte[] { 0x02 }, SpecialCondition = "Празнично работно време" }, // бит 1
                    new RestaurantSpecialConditions { Status = new byte[] { 0x02 }, SpecialCondition = "Само телефонни резервации" }, //бит 1
                    new RestaurantSpecialConditions { Status = new byte[] { 0x02 }, SpecialCondition = "Частно събитие" }, //бит 1
                    new RestaurantSpecialConditions { Status = new byte[] { 0x02 }, SpecialCondition = "Почивен ден" }, // бит 1
                    new RestaurantSpecialConditions { Status = new byte[] { 0x02 }, SpecialCondition = "Технически проблем" }, // бит 1
                    new RestaurantSpecialConditions { Status = new byte[] { 0x00 }, SpecialCondition = "Жива музика" }, // бит 2
                    new RestaurantSpecialConditions { Status = new byte[] { 0x00 }, SpecialCondition = "Специална вечер" } // бит 2
                );
            }

            if (!context.RestaurantTableTypes.Any())
            {
                await context.RestaurantTableTypes.AddRangeAsync(
                    new RestaurantTableTypes { TableType = "Бар", Seats = 1 },
                    new RestaurantTableTypes { TableType = "Маса за двама", Seats = 2 },
                    new RestaurantTableTypes { TableType = "Маса за трима", Seats = 3 },
                    new RestaurantTableTypes { TableType = "Маса за четирима", Seats = 4 },
                    new RestaurantTableTypes { TableType = "Маса за шестима", Seats = 6 },
                    new RestaurantTableTypes { TableType = "Маса за осем", Seats = 8 },
                    new RestaurantTableTypes { TableType = "Маса за десет", Seats = 10 },
                    new RestaurantTableTypes { TableType = "Маса за петнадесет", Seats = 15 },
                    new RestaurantTableTypes { TableType = "Малко сепаре", Seats = 10 },
                    new RestaurantTableTypes { TableType = "Средно сепаре", Seats = 15 },
                    new RestaurantTableTypes { TableType = "Голямо сепаре", Seats = 20 },
                    new RestaurantTableTypes { TableType = "Малка шатра", Seats = 8 },
                    new RestaurantTableTypes { TableType = "Средна шатра", Seats = 14 },
                    new RestaurantTableTypes { TableType = "Голяма шатра", Seats = 20 }
                );
            }
        }
    }
}
