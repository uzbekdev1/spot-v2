namespace SpotApp.Core
{
    internal class AppSettings
    {

#if DEBUG
        public const string ApiUrl = "http://172.16.14.27:5001";
#else
        public const string ApiUrl = "http://oldspot-api.uzex.uz";
#endif

        public const string AppName = "SpotApp";

        public const string AppVersion = "19062024";

    }
}
