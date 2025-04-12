namespace RestaurantReservation.Repository
{
    public interface IRepository<T> where T : class
    {
        /// <summary>Достъп до запис по Ид </summary>
        Task<T> GetByIdAsync(int id);

        /// <summary> Извличане на всички записи </summary>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary> Добавяне </summary>
        Task AddAsync(T entity);

        /// <summary> Редакция </summary>
        void Update(T entity);

        /// <summary> Изтриване </summary>
        void Delete(T entity);
    }
}
