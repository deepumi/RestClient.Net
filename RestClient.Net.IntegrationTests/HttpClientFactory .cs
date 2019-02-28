using System.Net.Http;
using System.Threading;

namespace RestClient.Net.IntegrationTests
{
    internal sealed class HttpClientFactory
    {
        private static readonly HttpClient _client = new HttpClient(new HttpClientHandler { AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate }, false);

        static HttpClientFactory()
        {
            //set base URL if possible
            _client.DefaultRequestHeaders.ExpectContinue = false;
            _client.Timeout = Timeout.InfiniteTimeSpan;
        }

        internal HttpClient Client => _client;
    }
}
