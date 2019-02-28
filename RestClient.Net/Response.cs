using System.Net;
using System.Net.Http.Headers;

namespace RestClient.Net
{
    public sealed class Response
    {
        private readonly Headers _headers = new Headers();

        private readonly HttpResponseHeaders _httpResponseHeaders;

        private readonly HttpContentHeaders _httpContentHeaders;

        public HttpStatusCode HttpStatusCode { get; }

        internal Response(HttpResponseHeaders httpResponseHeaders, HttpContentHeaders httpContentHeaders, HttpStatusCode statusCode)
        {
            HttpStatusCode = statusCode;

            _httpResponseHeaders = httpResponseHeaders;

            _httpContentHeaders = httpContentHeaders;
        }

        public Headers GetHeaders()
        {
            if (_httpResponseHeaders != null) _headers.AddRange(_httpResponseHeaders);

            if (_httpContentHeaders != null) _headers.AddRange(_httpContentHeaders);

            return _headers;
        }
    }
}
