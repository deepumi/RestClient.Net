using System.Net.Http;

namespace RestClient.Net
{
    public sealed class RestClientDefaultSettings
    {
        public HttpClient HttpClient { get; set; }

        public IMessageResolver DefaultMessageResolver { get; set; }

        internal string BearerToken { get; private set; }

        internal string BasicCredentials { get; private set; }

        internal bool Intialized { get; private set; }

        public void AddBearerToken(string token) => BearerToken = token;

        public void AddBasicAuthentication(string username, string password) => BasicCredentials = $"{username}:{password}";

        internal void EnsureInitialized()
        {
            if (Intialized) return;

            Intialized = true;
        }
    }
}
