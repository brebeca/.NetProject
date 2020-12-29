using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TsenseWebApp.Config;
using System.Linq;

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
            List<Sentiment> body = (from string text in texts
                                    select new Sentiment(text)).ToList();

            HttpResponseMessage res = await _httpClient.PostAsync(Constants.MultiplePredictionsUrl,
                new StringContent(
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
            HttpResponseMessage res = await _httpClient.PostAsync(Constants.SinglePredictionUrl, new StringContent(
                JsonSerializer.Serialize(new Sentiment(text)),
                Encoding.UTF8, "application/json"
               ));

            HttpContent content = res.Content;
            JObject data = JObject.Parse(await content.ReadAsStringAsync());

            return data;
        }
    }
}
