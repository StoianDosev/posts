using System.Net.Http;
using System.Threading.Tasks;
using WebApi.Responses;

namespace WebApi.Clients
{
    public class JsonplaceholderClient : IApiClient
    {

        private readonly IHttpClientFactory _factory;
        private readonly string url = "https://jsonplaceholder.typicode.com/";
        private readonly HttpClient _client;
        public JsonplaceholderClient(IHttpClientFactory factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }


        public async Task<ResponseModel> GetAll(string name)
        {
            string getUrl = $"{url}{name}";

            HttpResponseMessage result = await _client.GetAsync(getUrl);
            string jsonResult = await result.Content.ReadAsStringAsync();
            return new ResponseModel { Key = name, JsonValue = jsonResult };
        }


    }
}