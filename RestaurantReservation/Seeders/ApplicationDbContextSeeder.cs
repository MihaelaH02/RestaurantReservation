using Microsoft.AspNetCore.Hosting.Server;

namespace RestaurantReservation.Seeders
{
    public class ApplicationDbContextSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            /// <summary> Добавяне на първоначални данни (Seeder)  </summary>
            var seeders = new List<ISeeder>
            {
                new NomenclaturesSeeder(),
                new InitDefault()     
            };

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync(dbContext);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}