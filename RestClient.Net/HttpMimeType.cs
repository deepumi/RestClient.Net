using System;

namespace RestClient.Net
{
    public sealed class HttpMimeType
    {
        /// <summary>
        /// Return application/json as result content type.
        /// </summary>
        public static HttpMimeType Json { get; } = new HttpMimeType("application/json");

        /// <summary>
        /// Return application/xml as result content type.
        /// </summary>
        public static HttpMimeType Xml { get; } = new HttpMimeType("application/xml");

        internal string ContentType { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:XPAND.CareerOneStop.Portal.Core.HttpContentType" /> class with a specific HTTP method.
        /// </summary>
        /// <param name="contentType"></param>
        public HttpMimeType(string contentType)
        {
            if (string.IsNullOrEmpty(contentType)) throw new ArgumentException(nameof(contentType));

            ContentType = contentType;
        }
    }
}
