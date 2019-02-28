using System;

namespace RestClient.Net
{
    public static class RestClientConfiguration
    {
        //private static readonly Lazy<RestClientConfiguration> _configuration = 
        //    new Lazy<RestClientConfiguration>(() => new RestClientConfiguration());

        //internal static RestClientConfiguration Configuration = _configuration.Value;

        internal static readonly RestClientDefaultSettings Configuration = new RestClientDefaultSettings();

        public static void Configure(Action<RestClientDefaultSettings> configure)
        {
            configure(Configuration);

            Configuration.EnsureInitialized();
        }
    }
}
