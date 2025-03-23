using Microsoft.AspNetCore.Identity;
using RestaurantReservation.Common;
using RestaurantReservation.Models.Nomenclatures;
using RestaurantReservation.Models.RestaurantsSettings;
using RestaurantReservation.Models.System;
using RestaurantReservation.Models.Users;

namespace RestaurantReservation.Seeders
{
    public class InitDefault : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext context)
        {

            if (!context.RestaurantScheduleSettings.Any())
            {
                RestaurantSpecialConditions noneSpecialCondition = context.RestaurantSpecialConditions.First(x => x.SpecialCondition == "Няма");
                //Restaurants noneRestaurant = context.Restaurants.Find(x => x.Account == 0);

                await context.RestaurantScheduleSettings
                    .AddAsync(new RestaurantScheduleSettings()
                    {
                        SpecificDate = GlobalConstants.NON_DATE,
                        Restaurant = 0, // default за всички ресторанти 
                        WeekDay = 0, // default за всеки ден от седмицата
                        HourFrom = 9, 
                        HourTo = 11, 
                        RestaurantSpecialCondition = noneSpecialCondition
                    }
                );
            }

            if (!context.Accounts.Any())
            {
                var admin = new Accounts()
                {
                    Status = new byte[] { 0x02 }, // бит 1
                    Role = context.AccountRoleTypes.First(x => x.Role == "Администратор"),
                    Username = "Adminov",
                    Email = "adm@abv.bg",
                    Phone = "123456789",
                    AccessFailCount = 0,
                    CreatedAt = DateTime.Now,
                    LastChangeAt = DateTime.Now,
                    BlockedAt = GlobalConstants.NON_DATE,
                };
                admin.Password = new PasswordHasher<Accounts>().HashPassword(admin, "Admin123!");

                await context.Accounts.AddAsync(admin);
            }

            if (!context.NotificationSettings.Any())
            {
                await context.NotificationSettings.AddRangeAsync(
                    new NotificationSettings
                    {
                     // Restaurant = 0;
                        Event = context.NotificationEvents.First(x => x.Event == "Регистрация"),
                        Title = "{APP_NAME} - Успешна регистрация",
                        Description = "Здравейте, {NAME_USER},\r\n\r\nБлагодарим Ви, че се регистрирахте в нашата система!  \r\nЗа да активирате профила си, моля, натиснете върху линка по-долу:\r\n\r\n{LINK}.\r\n\r\nПоздрави, \r\nЕкипът на {APP_NAME}"
                    },
                    new NotificationSettings
                    {
                     // Restaurant = 0;
                        Event = context.NotificationEvents.First(x => x.Event == "Смяна на парола"),
                        Title = "{APP_NAME} - Заявка за смяна на парола",
                        Description = "Здравейте, {NAME_USER},\r\n\r\nПолучихме заявка за смяна на Вашата парола.  \r\nАко искате да я смените, натиснете върху линка:\r\n\r\n{LINK}\r\n\r\nПоздрави,\r\nЕкипът на {APP_NAME}"
                    },
                    new NotificationSettings
                    {
                     // Restaurant = 0;
                        Event = context.NotificationEvents.First(x => x.Event == "Блокиран потребител"),
                        Title = "{APP_NAME} - Вашият профил е временно блокиран ",
                        Description = "Здравейте, {NAME_USER},\r\n\r\nВашият профил в {APP_NAME} беше временно блокиран поради превишен брой опити за влизане с грешна парова в профила Ви. За да активирате отново профила си, моля, натиснете върху линка по-долу за са смените Вашета парола:\r\n\r\n{LINK}. \r\n\r\nПоздрави,\r\nЕкипът на {APP_NAME}"
                    },
                    new NotificationSettings
                    {
                     // Restaurant = 0;
                        Event = context.NotificationEvents.First(x => x.Event == "Заявена резервация"),
                        Title = "{APP_NAME} - Вашата резервация е получена",
                        Description = "Здравейте, {NAME_USER},\r\n\r\nБлагодарим Ви, че направихте заявка за резервация при нас!  \r\nДетайли за резервацията:\r\n- Ресторант: {RESTAURANT_NAME}\r\nЛокация: {LOCATION}\r\n- Дата и час: {DATE_TIME}\r\n- Брой гости: {GUESTS}\r\n\r\nЩе Ви уведомим при потвърждение.\r\n\r\nПоздрави, \r\nЕкипът на {APP_NAME}"
                    },
                    new NotificationSettings
                    {
                    // Restaurant = 0;
                        Event = context.NotificationEvents.First(x => x.Event == "Потвърдена резервация"),
                        Title = "{APP_NAME} - Вашата резервация е потвърдена",
                        Description = "Здравейте, {NAME_USER},\r\n\r\nВашата резервация е потвърдена успешно!  \r\nДетайли:\r\n- Ресторант: {RESTAURANT_NAME}\r\n- Локация: {LOCATION}\r\n- Дата и час: {DATE_TIME}\r\n- Брой гости: {GUESTS}\r\n\r\n Можете да откриете повече информация на: {LINK}.\r\n\r\nОчакваме Ви!\r\n\r\nПоздрави, \r\nЕкипът на {APP_NAME}"
                    },
                    new NotificationSettings
                    {
                    // Restaurant = 0;
                        Event = context.NotificationEvents.First(x => x.Event == "Отказана резервация"),
                        Title = "{APP_NAME} - Вашата резервация е отказана",
                        Description = "Здравейте, {NAME_USER},\r\n\r\nЗа съжаление, Вашата резервация в {RESTAURANT_NAME} беше отказана. \r\nИзвиняваме се за пречиненото неудобство. Очакваме Ви онтово сроко. \r\n\r\nАко имате въпроси, свържете се с нас.\r\n\r\nПоздрави,  \r\nЕкипът на {APP_NAME}"
                    },
                    new NotificationSettings
                    {
                    // Restaurant = 0;
                        Event = context.NotificationEvents.First(x => x.Event == "Призив за обратна връзка"),
                        Title = "{APP_NAME} - Как беше Вашето преживяване в {RESTAURANT_NAME}?",
                        Description = "Здравейте, {NAME_USER},\r\n\r\nНадяваме се, че сте имали приятно преживяване в {RESTAURANT_NAME}!  \r\nЩе се радваме, ако оставите обратна връзка и оценка:\r\n\r\n{LINK_TO_FEEDBACK}\r\n\r\nВашето мнение е важно за нас!\r\n\r\nПоздрави,  \r\nЕкипът на {APP_NAME}"
                    },
                    new NotificationSettings
                    {
                    // Restaurant = 0;
                        Event = context.NotificationEvents.First(x => x.Event == "Отнети точки"),
                        Title = "{APP_NAME} - Загубени точки в програмата за лоялност",
                        Description = "Здравейте, {NAME_USER},\r\n\r\nЗа съжаление Ви информираме за отнемане на {POINTS_LOST} точки, от програмата ни за лоялност, поради Вашето непосещение в {RESTAURANT_NAME}, на {DATE_TIME}.  \r\nМожете да проверите Вашия баланс тук:  \r\n\r\n{LINK_TO_ACCOUNT}.\r\n\r\n Ако е възникнала грешка, моля свържете се с екипа на {RESTAURANT_NAME} за повече информация на телефон {PHONE}. \r\n\r\nПоздрави,  \r\nЕкипът на {APP_NAME}"
                    },
                    new NotificationSettings
                    {
                    // Restaurant = 0;
                        Event = context.NotificationEvents.First(x => x.Event == "Информация"),
                        Title = "{APP_NAME} -  Важна информация",
                        Description = "Здравейте, {NAME_USER},\r\n\r\nИмаме важно съобщение за Вас:  \r\n\r\n{INFO_MESSAGE}\r\n\r\nПовече информация можете да намерите тук: {LINK}\r\n\r\nПоздрави,  \r\nЕкипът на {APP_NAME}"
                    },
                    new NotificationSettings
                    {
                     // Restaurant = 0;
                        Event = context.NotificationEvents.First(x => x.Event == "Друго"),
                        Title = "{TITLE}",
                        Description = "{DESCRIPTION}"
                    }
                );


            }
        }
    }
}
