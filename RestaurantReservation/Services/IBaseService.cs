
using RestaurantReservation.Repository;

namespace RestaurantReservation.Services
{
    public interface IBaseService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(short id);
        Task AddAsync(T type);
        Task UpdateAsync(short id, T type);
        Task DeleteAsync(short id);
    }
}
