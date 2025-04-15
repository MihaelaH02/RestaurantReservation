using Microsoft.EntityFrameworkCore;

namespace RestaurantReservation.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        Task SaveChangesAsync();

        /// <summary>Достъп до запис по Ид </summary>
        Task<T> GetByIdAsync(short id);

        /// <summary> Извличане на всички записи </summary>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary> Добавяне </summary>
        Task AddAsync(T entity);

        /// <summary> Редакция </summary>
        Task Update(T entity);

        /// <summary> Изтриване </summary>
        Task Delete(T entity);
    }
}
