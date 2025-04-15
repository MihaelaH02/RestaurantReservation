using Microsoft.EntityFrameworkCore;

namespace RestaurantReservation.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T> GetByIdAsync(short id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();


        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await SaveChangesAsync();

        }

        public async Task Update(T entity)
        {
            _dbSet.Update(entity);
            await SaveChangesAsync();

        }

        public async Task Delete(T entity)
        {
            _dbSet.Remove(entity);
            await SaveChangesAsync();
        }
    }

}
