using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace RestClient.Net
{
    public sealed class RestClient
    {
        private readonly HttpClient _client;

        private readonly RestClientDefaultSettings _configuration;

        public RestClient() : this(RestClientConfiguration.Configuration) { }

        private RestClient(RestClientDefaultSettings configuration) : this(configuration.HttpClient) => _configuration = configuration;

        public RestClient(HttpClient client) => _client = client ?? throw new ArgumentNullException(nameof(client));

        public Task<TResult> GetAsync<TResult>(Request requet, CancellationToken cancellationToken = default(CancellationToken), IMessageResolver messageResolver = null)
        {
            return ExecuteAsync<TResult>(requet, HttpMethod.Get, cancellationToken, messageResolver);
        }

        public Task<TResult> PostAsync<TResult>(Request requet, CancellationToken cancellationToken = default(CancellationToken), IMessageResolver messageResolver = null)
        {
            return ExecuteAsync<TResult>(requet, HttpMethod.Post, cancellationToken, messageResolver);
        }

        public async Task<TResult> ExecuteAsync<TResult>(Request request, HttpMethod httpMethod, CancellationToken cancellationToken = default(CancellationToken), IMessageResolver messageResolver = null)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var resolver = messageResolver.GetMessageResolver(_configuration);

            request.HttpMethod = httpMethod;

            using (var response = await _client.SendAsync(request.BuildRequest(_configuration),
                HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false))
            {
                if (response == null) return default(TResult);

                request.Response = new Response(response.Headers, response.Content?.Headers, response.StatusCode);

                if (!response.IsSuccessStatusCode) return default(TResult);

                using (var result = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                {
                    return resolver.Deserialize<TResult>(result, response.StatusCode);
                }
            }
        }
    }
}
