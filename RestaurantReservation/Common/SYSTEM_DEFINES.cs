namespace RestaurantReservation.Common
{
    public static class SYSTEM_DEFINES
    {
        public static readonly string APP_NAME              = "SmartSeat";
        public static readonly DateTime NON_DATE            = new DateTime(2000, 01, 01, 0, 0, 0, 0);
        public static readonly string STATUS_ERROR_LOG      = "ERROR";
        public static readonly string STATUS_WARRNING_LOG   = "WARRNING";
        public static readonly string STATUS_INFO_LOG       = "INFO";

    }

    public static class ERROR_MSG
    {
        //Акаунти
        public static readonly string MSG_ACCOUNTS_REGISTER_USERNAME_TAKEN      = "Потребителско име '{USERNAME}' е заето.";
        public static readonly string MSG_ACCOUNTS_REGISTER_EMAIL_TAKEN         = "Друг потребител е вписан в системата с имейл адрес '{EMAIL}'.";
        public static readonly string MSG_ACCOUNTS_REGISTER_PHONE_TAKEN         = "Друг потребител е вписан в системата с телефонен номер '{PHONE}'.";
        public static readonly string MSG_ACCOUNTS_LOGIN_WRONG_USERNAME         = "Грешно потребителско име.";
        public static readonly string MSG_ACCOUNTS_LOGIN_WRONG_PASSWORD         = "Грешна парола.";
        public static readonly string MSG_ACCOUNTS_LOGIN_USER_BLOCKED           = "Потребителят е блокиран.";
        public static readonly string MSG_ACCOUNTS_PASWORD_RESET_WRONG_EMAIL    = "Невалиден имейл адрес.";
        public static readonly string MSG_ACCOUNTS_PASWORD_RESET_WRONG_TOKEN    = "Невалиден код.";

        //Ресторанти
        public static readonly string MSG_RESTAURNATS_SEARCH_BY_FILERS_NONE     = "Не бяха открити резултати по зададените критерии.";


    }
}
