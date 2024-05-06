namespace SpotApp.Core
{
    internal class AppSettings
    {

#if DEBUG
        public const string ApiUrl = "http://testoldspot-api.uzex.uz";
#else
        public const string ApiUrl = "http://oldspot-api.uzex.uz";
#endif

        public const string AppName = "SpotApp";

        public const string AppVersion = "30042024";

    }
}
