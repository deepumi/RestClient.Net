using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace RestClient.Net
{
    public sealed class Request
    {
        private readonly Uri _uri;

        private AuthorizationHeader _authHeader;

        private FormPostParameter _formPostParameter;

        private Headers _headers;

        internal HttpMethod HttpMethod { get; set; }

        public TimeSpan TimeOut { get; set; }

        public HttpMimeType MimeType { get; set; } = HttpMimeType.Json;

        public Response Response { get; internal set; }

        public object HttpContent { get; set; }

        public Request(Uri uri) => _uri = uri ?? throw new ArgumentNullException(nameof(uri));

        public Request(string url) : this(new Uri(url ?? throw new ArgumentNullException(nameof(url)))) { }

        public void AddHeader(string name, string value)
        {
            if(_headers == null) _headers = new Headers();

            _headers.Add(name,value);
        }

        public void AddBearerToken(string token)
        {
            if(_authHeader == null) _authHeader = new AuthorizationHeader();

            _authHeader.AddBearerToken(token);
        }

        public void AddBasicAuthentication(string username, string password)
        {
            if (_authHeader == null) _authHeader = new AuthorizationHeader();

            _authHeader.AddBasicAuthentication(username, password);
        }

        public void AddFormValue(string name, string value)
        {
            if(name == null) return;

            if(_formPostParameter == null) _formPostParameter = new FormPostParameter();

            _formPostParameter.AddFormValue(name, value);
        }

        public void AddFormValue(IDictionary<string, string> dictionary)
        {
            if (dictionary == null) return;

            if (_formPostParameter == null) _formPostParameter = new FormPostParameter();

            _formPostParameter.AddFormValue(dictionary);
        }
 
        internal HttpRequestMessage BuildRequest(RestClientDefaultSettings configuration)
        {
            var req = new HttpRequestMessage(HttpMethod, _uri);

            req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(MimeType.ContentType));

            if (_authHeader != null)
                req.Headers.Authorization = _authHeader.Create(configuration);

            if (_headers != null)
            {
                foreach (var item in _headers)
                    req.Headers.Add(item.Key, item.Value);
            }

            if (HttpMethod != HttpMethod.Get)
            {
                if (_formPostParameter?.FormContent != null)
                {
                    req.Content = GetHttpContent(_formPostParameter.FormContent);
                }
                else if (HttpContent != null)
                {
                    req.Content = GetHttpContent(HttpContent);
                }
            }

            return req;
        }

        private HttpContent GetHttpContent(object content)
        {
            switch (content)
            {
                case string s :
                    return new StringContent(s);

                case HttpContent http:
                    return http;

                case byte[] bytes:
                    return new ByteArrayContent(bytes);

                case Stream stream:
                    return new StreamContent(stream);

                default:
                    return new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, MimeType.ContentType);
            }
        }
    }
}
