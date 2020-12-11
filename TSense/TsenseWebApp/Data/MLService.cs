using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TsenseWebApp.Data
{
    public class MLService
    {
        private readonly HttpClient _httpClient;

        public MLService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<JObject> SentimentFromLink(string text)
        {
            HttpResponseMessage res = await _httpClient.PostAsync("http://localhost:5000/api/v1/predictions", new StringContent(
                JsonSerializer.Serialize(new Sentiment(text)),
                Encoding.UTF8, "application/json"
               ));

            HttpContent content = res.Content;
            JObject data = JObject.Parse(await content.ReadAsStringAsync());

            return data;
        }

        public async Task<JObject> SentimentFromText(string text)
        {
            HttpResponseMessage res = await _httpClient.PostAsync("http://localhost:5000/api/v1/predictions", new StringContent(
                JsonSerializer.Serialize(new Sentiment(text)),
                Encoding.UTF8, "application/json"
               ));

            HttpContent content = res.Content;
            JObject data = JObject.Parse(await content.ReadAsStringAsync());

            return data;
        }
    }
}
