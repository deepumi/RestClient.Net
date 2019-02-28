using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace RestClient.Net
{
    public sealed class JsonMessageResolver : IMessageResolver
    {
        private readonly JsonSerializer _serializer = new JsonSerializer();

        public T Deserialize<T>(Stream stream, HttpStatusCode stausCode)
        {
            if (stream == null || !stream.CanRead) return default(T);

            using (var reader = new StreamReader(stream))
            {
                using (var json = new JsonTextReader(reader))
                {
                    return _serializer.Deserialize<T>(json);
                }
            }
        }
    }
}
