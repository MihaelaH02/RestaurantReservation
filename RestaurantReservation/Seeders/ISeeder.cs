namespace RestaurantReservation.Seeders
{
    public interface ISeeder
    {
        Task SeedAsync(ApplicationDbContext dbContext);
    }
}
