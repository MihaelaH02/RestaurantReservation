using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RestaurantReservation.Models.AdditionalFunctionality
{
    public enum FilterType : short
    {
        Atmosphere = 1,
        Location,
        Date,
        Rate
    }

    [Keyless]
    [Table("ARG_RESTAURANT_SEARCH_FILTERS")]
    public class RestaurantSearchFilter
    {
        public RestaurantSearchFilter() { }

        public RestaurantSearchFilter(Guid Id, FilterType filterType, string filterValue) 
        {
            UUID = Id;
            FilterType = (short)filterType;
            FilterValue = filterValue;
        }
        ~RestaurantSearchFilter() { }

        [Key]
        [Column("UUID")]
        public Guid UUID { get; set; }

        [Column("FILER_TYPE")]
        public short FilterType { get; set; }

        [Column("FILTER_VALUE")]
        public string FilterValue { get; set; }
    }

}
