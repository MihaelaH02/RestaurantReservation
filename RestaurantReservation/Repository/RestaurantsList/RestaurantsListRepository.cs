using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RestaurantReservation.DTO.Restaurants;
using RestaurantReservation.Models.AdditionalFunctionality;
using System.Data;
using RestaurantReservation.Models.Nomenclatures;

namespace RestaurantReservation.Repository.RestaurantsList.RestaurantsList
{
    public class RestaurantsListRepository : ISearchRestaurantRepository
    {
        private readonly ApplicationDbContext _context;
        private const String SP_SEARCH_RESTAURANTS_BY_FILTERS = "SP_SEARCH_RESTAURANTS_BY_FILTERS";

        public RestaurantsListRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<RestaurantsListsResponseDTO>> GetFilteredRestaurantsAsync(Guid uuid)
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = SP_SEARCH_RESTAURANTS_BY_FILTERS;
                command.CommandType = CommandType.StoredProcedure;

                var uuidParam = new SqlParameter("@UUID", uuid);
                command.Parameters.Add(uuidParam);

                await _context.Database.OpenConnectionAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    var result = new List<RestaurantsListsResponseDTO>();

                    while (await reader.ReadAsync())
                    {
                        RestaurantAtmosphere? athmosphere = await _context.RestaurantAtmospheres
                                                    .FirstOrDefaultAsync(a => a.Id == 
                                                        reader.GetInt16(reader.GetOrdinal("ATMOSPHERE_ID")));
                        if (athmosphere != null)
                            throw new ArgumentNullException(nameof(athmosphere));

                        result.Add(new RestaurantsListsResponseDTO
                        {
                            Id = reader.GetInt16(reader.GetOrdinal("ID")),
                            CompanyName = reader.GetString(reader.GetOrdinal("COMPANY_NAME")),
                            Address = reader.GetString(reader.GetOrdinal("ADDRESS")),
                            Rating = reader.GetFloat(reader.GetOrdinal("RATING")),
                            Atmosphere = athmosphere
                        });
                    }

                    return result;
                }
            }
        }

        public async Task SaveSearchFiltersAsync(Guid uuid, List<RestaurantSearchFilter> filters)
        {
            _context.RestaurantSearchFilter.AddRange(filters);
            await _context.SaveChangesAsync();
        }
        public async Task ClearSearchFiltersAsync(Guid uuid)
        {
            var filtersToDelete = _context.RestaurantSearchFilter
                                            .Where(f => f.UUID == uuid);

            _context.RestaurantSearchFilter.RemoveRange(filtersToDelete);

            await _context.SaveChangesAsync();
        }
    }
}