using System.IO;
using System.Net;

namespace RestClient.Net
{
    public interface IMessageResolver
    {
        T Deserialize<T>(Stream stream, HttpStatusCode stausCode);
    }
}
