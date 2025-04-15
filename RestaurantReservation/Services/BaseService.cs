using RestaurantReservation.Models.Users;
using RestaurantReservation.Repository;

namespace RestaurantReservation.Services
{
    public class RestaurantService<T> : IBaseService<T> where T : class
    {
        private readonly IBaseRepository<T>  repository;

        public RestaurantService(IBaseRepository<T> repository)
        {
            this.repository = repository;
        }
        public async Task<IEnumerable<T>> GetAllAsync() => await repository.GetAllAsync();

        public async Task<T?> GetByIdAsync(short id) => await repository.GetByIdAsync(id);

        public async Task AddAsync(T type)
        {
            await repository.AddAsync(type);
        }

        public async Task UpdateAsync(short id, T type)
        {
            T? restaurant = await GetByIdAsync(id);
            if (restaurant == null) 
                throw new ArgumentNullException(nameof(restaurant));

            restaurant = type;
            repository.Update(restaurant);
        }

        public async Task DeleteAsync(short id)
        {
            T? restaurant = await repository.GetByIdAsync(id);
            if (restaurant == null)
                throw new ArgumentNullException(nameof(restaurant));

            repository.Delete(restaurant);
        }
    }
}
