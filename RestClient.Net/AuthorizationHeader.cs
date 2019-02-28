using System;
using System.Net.Http.Headers;
using System.Text;

namespace RestClient.Net
{
    internal sealed class AuthorizationHeader
    {
        private string _bearerToken;

        private string _credentials;

        internal void AddBearerToken(string token) => _bearerToken = token;

        internal void AddBasicAuthentication(string username, string password) => _credentials = $"{username}:{password}";

        internal AuthenticationHeaderValue Create(RestClientDefaultSettings configuration)
        {
            var authentication = default(AuthenticationHeaderValue);

            if (!string.IsNullOrEmpty(configuration?.BasicCredentials) && string.IsNullOrEmpty(_credentials))
                authentication = CreateBasic(configuration.BasicCredentials);

            if (!string.IsNullOrEmpty(_credentials))
                authentication = CreateBasic(_credentials);

            if (!string.IsNullOrEmpty(configuration?.BearerToken) && string.IsNullOrEmpty(_bearerToken))
                authentication = CreateBearer(configuration.BearerToken);

            if (!string.IsNullOrEmpty(_bearerToken))
                authentication = CreateBearer(_bearerToken);

            return authentication;
        }

        private static AuthenticationHeaderValue CreateBasic(string credentials) => new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials)));

        private static AuthenticationHeaderValue CreateBearer(string bearerToken) => new AuthenticationHeaderValue("Bearer", bearerToken);
    }
}
