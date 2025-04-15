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

                // default Restaurant = null; - за всички ресторанти
                await context.RestaurantScheduleSettings
                    .AddAsync(new RestaurantScheduleSettings()
                    {
                        Restaurant = null,
                        SpecificDate = SYSTEM_DEFINES.NON_DATE,
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
                    Status = GlobalMethods.SetBit(new byte[] { 0x00 }, AccountStatusBits.STS_ACTIVE, true),
                    Role = context.AccountRoleTypes.First(x => x.Role == "Admin"),
                    Username = "Admin",
                    Email = "adm@abv.bg",
                    Phone = "123456789",
                    AccessFailCount = 0,
                    CurrentAccessFailCount = 0,
                    CreatedAt = DateTime.Now,
                    LastChangeAt = DateTime.Now,
                    BlockedAt = SYSTEM_DEFINES.NON_DATE,
                };
                admin.Password = new PasswordHasher<Accounts>().HashPassword(admin, "Admin123!");

                await context.Accounts.AddAsync(admin);
            }

            if (!context.NotificationSettings.Any())
            {
                await context.NotificationSettings.AddRangeAsync(
                    new NotificationSettings
                    {
                        Restaurant = null,
                        Event = context.NotificationEvents.First(x => x.Event == "Регистрация"),
                        Title = "{APP_NAME} - Успешна регистрация",
                        Description = "Здравейте, {NAME_USER},<br><br> Благодарим Ви, че се регистрирахте в нашата система! <br>За да активирате профила си, моля, натиснете върху линка по-долу: {LINK}.<br><br>Поздрави, Екипът на {APP_NAME}"
                    },
                    new NotificationSettings
                    {
                        Restaurant = null,
                        Event = context.NotificationEvents.First(x => x.Event == "Смяна на парола"),
                        Title = "{APP_NAME} - Заявка за смяна на парола",
                        Description = "Здравейте, {NAME_USER},<br><br>Получихме заявка за смяна на Вашата парола.<br>Моля използвайте временната парола \"{CODE}\" за да достъпите Вашия профил.<br><br>Поздрави,\r\nЕкипът на {APP_NAME}"
                    },
                    new NotificationSettings
                    {
                        Restaurant = null,
                        Event = context.NotificationEvents.First(x => x.Event == "Блокиран потребител"),
                        Title = "{APP_NAME} - Вашият профил е временно блокиран ",
                        Description = "Здравейте, {NAME_USER},<br><br>Вашият профил в {APP_NAME} беше временно блокиран поради превишен брой опити за влизане с грешна парола в профила Ви. За да активирате отново профила си, моля, натиснете върху линка по-долу за са смените Вашета парола:<br> {LINK}. <br>Поздрави, <br>Екипът на {APP_NAME}"
                    },
                    new NotificationSettings
                    {
                        Restaurant = null,
                        Event = context.NotificationEvents.First(x => x.Event == "Заявена резервация"),
                        Title = "{APP_NAME} - Вашата резервация е получена",
                        Description = "Здравейте, {NAME_USER},<br><br>Благодарим Ви, че направихте заявка за резервация при нас! <br>Детайли за резервацията:<br>       - Ресторант: {RESTAURANT_NAME}<br>        -Локация: {LOCATION}<br>       - Дата и час: {DATE_TIME}<br>       - Брой гости: {GUESTS}<br>Ще Ви уведомим при потвърждение.<br><br>Поздрави,<br> Екипът на {APP_NAME}"
                    },
                    new NotificationSettings
                    {
                        Restaurant = null,
                        Event = context.NotificationEvents.First(x => x.Event == "Потвърдена резервация"),
                        Title = "{APP_NAME} - Вашата резервация е потвърдена",
                        Description = "Здравейте, {NAME_USER},<br><br>Вашата резервация е потвърдена успешно!  <br>Детайли:<br>       - Ресторант: {RESTAURANT_NAME}<br>       - Локация: {LOCATION}<br>       - Дата и час: {DATE_TIME}<br>       - Брой гости: {GUESTS}<br> Масата Ви ще се пази до {KEEP_RES} <br> Можете да откриете повече информация на: {LINK}.<br>Очакваме Ви!<br><br> Поздрави, <br>Екипът на {APP_NAME}"
                    },
                    new NotificationSettings
                    {
                        Restaurant = null,
                        Event = context.NotificationEvents.First(x => x.Event == "Отказана резервация"),
                        Title = "{APP_NAME} - Вашата резервация е отказана",
                        Description = "Здравейте, {NAME_USER},<br><br>За съжаление, Вашата резервация в {RESTAURANT_NAME} беше отказана. <br>Извиняваме се за пречиненото неудобство. Очакваме Ви онтово сроко. <br>Ако имате въпроси, свържете се с нас.<br>Поздрави, <br>Екипът на {APP_NAME}"
                    },
                    new NotificationSettings
                    {
                        Restaurant = null,
                        Event = context.NotificationEvents.First(x => x.Event == "Призив за обратна връзка"),
                        Title = "{APP_NAME} - Как беше Вашето преживяване в {RESTAURANT_NAME}?",
                        Description = "Здравейте, {NAME_USER},<br><br>Надяваме се, че сте имали приятно преживяване в {RESTAURANT_NAME}!  <br>Ще се радваме, ако оставите обратна връзка и оценка:<br>{LINK_TO_FEEDBACK}<br>Вашето мнение е важно за нас!<br>Поздрави,<br> Екипът на {APP_NAME}"
                    },
                    new NotificationSettings
                    {
                        Restaurant = null,
                        Event = context.NotificationEvents.First(x => x.Event == "Отнети точки"),
                        Title = "{APP_NAME} - Загубени точки в програмата за лоялност",
                        Description = "Здравейте, {NAME_USER},<br><br>За съжаление Ви информираме за отнемане на {POINTS_LOST} точки, от програмата ни за лоялност, поради Вашето непосещение в {RESTAURANT_NAME}, на {DATE_TIME}.  <br>Можете да проверите Вашия баланс тук:  <br>{LINK_TO_ACCOUNT}.<br> Ако е възникнала грешка, моля свържете се с екипа на {RESTAURANT_NAME} за повече информация на телефон {PHONE}. <br>Поздрави, <br>Екипът на {APP_NAME}"
                    },
                    new NotificationSettings
                    {
                        Restaurant = null,
                        Event = context.NotificationEvents.First(x => x.Event == "Информация"),
                        Title = "{APP_NAME} -  Важна информация",
                        Description = "Здравейте, {NAME_USER},<br><br>Имаме важно съобщение за Вас:  <br>{INFO_MESSAGE}<br>Повече информация можете да намерите тук: <br>{LINK}<br><br>Поздрави,  <br>Екипът на {APP_NAME}"
                    },
                    new NotificationSettings
                    {
                        Restaurant = null,
                        Event = context.NotificationEvents.First(x => x.Event == "Друго"),
                        Title = "{TITLE}",
                        Description = "{DESCRIPTION}"
                    }
                );
            }
        }
    }
}
