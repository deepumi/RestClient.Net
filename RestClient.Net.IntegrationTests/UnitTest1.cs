using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RestClient.Net.IntegrationTests
{
    [TestClass]
    public class UnitTest1
    {
        static UnitTest1()
        {
            RestClientConfiguration.Configure
            (
                c =>
                {
                    c.HttpClient = new HttpClientFactory().Client;
                }
            );
        }
         

        [TestMethod]
        public async Task TestMethod1()
        {
            var client = new RestClient();

            var req = new Request("https://someapi.com");
            req.AddFormValue("somename", "FFF");
            req.AddFormValue("sssdf", "sdfsdf");
            req.AddBearerToken("sometoken");

            var result = await client.PostAsync<Rootobject>(req);

            var header = req.Response.GetHeaders();

            var sss = header.GetFirstValue("Vary");
        }
    }

    public class Rootobject
    {
    }
}
