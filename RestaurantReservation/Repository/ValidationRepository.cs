using RestaurantReservation.Common;

namespace RestaurantReservation.Repository
{
    /// <summary> Интерфейс за валидации </summary>
    public interface ValidationRepository<T> where T : class
    {
        public Task<ErrorMsg?> Validate(T validateDTO);
    }
}
