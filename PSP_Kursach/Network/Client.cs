using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Network
{
    public class Client
    {
        private string _url;
        private HttpClient _httpClient;

        public Client(string url)
        {

            _httpClient = new HttpClient();
            _url = url;
        }

        public int ClientId { get; set; }

        public void Connect()
        {
            var responce = _httpClient.GetAsync(_url).Result;
            ClientId = int.Parse(responce.Content.ReadAsStringAsync().Result);
        }

        public async Task<T> Get<T>(string method)
        {
            var str = await _httpClient.GetAsync(_url + (method ?? string.Empty) + $"?id={ClientId}").Result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(str);
        }

        public async Task Set<T>(T obj, string method)
        {
            await _httpClient.PostAsync(_url + (method ?? string.Empty) + $"?id={ClientId}", new StringContent(JsonConvert.SerializeObject(obj)));
        }

        public int Start()
        {
            return int.Parse(_httpClient.GetAsync(_url + "/Start/").Result.Content.ReadAsStringAsync().Result);
        }
    }
}
