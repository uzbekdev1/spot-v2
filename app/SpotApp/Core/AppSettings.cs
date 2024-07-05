namespace SpotApp.Core
{
    internal class AppSettings
    {

#if DEBUG
        public const string ApiUrl = "http://172.16.14.27:5001";

        public const string ApiUrlDomen = "http://testoldspot-api.uzex.uz/";
#else
        public const string ApiUrl = "http://172.16.14.19:5001";

        public const string ApiUrlDomen = "http://oldspot-api.uzex.uz/";
#endif

        public const string AppName = "SpotApp";

        public const string AppVersion = "20062024";

    }
}
