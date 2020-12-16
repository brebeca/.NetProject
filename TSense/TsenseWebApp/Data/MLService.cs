using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TsenseWebApp.Data;

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

       
        public async Task<JObject> SentimentFromMultiple(List<string> texts)
        {
            List<Sentiment> body = new List<Sentiment>();
            foreach(string text in texts)
            {
                body.Add(new Sentiment(text));
            }

            HttpResponseMessage res = await _httpClient.PostAsync("http://localhost:5000/api/v1/predictions/multiple", new StringContent(
                JsonSerializer.Serialize(body),
                Encoding.UTF8, "application/json"
               ));
            if (res.IsSuccessStatusCode)
            {
                HttpContent content = res.Content;
                JObject data = JObject.Parse(await content.ReadAsStringAsync());
                return data;

            }
            return null;
            
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
