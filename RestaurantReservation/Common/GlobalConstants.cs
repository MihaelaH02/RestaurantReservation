namespace RestaurantReservation.Common
{
    public enum WeekDays
    {
        // [ComponentModel.Description("Example One")]
        Monday = 1,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }

    public static class GlobalConstants
    {
        public static readonly DateTime NON_DATE = new DateTime(2000, 01, 01, 0, 0, 0, 0);
    }

    public static class GlobalMethods
    {

        public static string GetEnumDescription(Enum enumElemen)//?
        {
            return enumElemen.ToString();
        }
    }
}
