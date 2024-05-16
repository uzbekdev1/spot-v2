namespace ClientApi.Core
{
    public static class RateLimiterPolicies
    {
        public const string fixed_2_limit_in_1_sec = "fixed_2_limit_in_1_sec";
        public const string fixed_3_limit_in_1_sec = "fixed_3_limit_in_1_sec";
        public const string fixed_5_limit_in_1_sec = "fixed_5_limit_in_1_sec";
    }
}
