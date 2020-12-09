using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using Newtonsoft.Json.Linq;

namespace TsenseWebApp.Data
{
    public class MLService
    {
        private readonly HttpClient _httpClient;

        public MLService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<string> Post(string text)
        {
            JObject o = JObject.Parse(text);
            HttpResponseMessage res = await _httpClient.PostAsync("http://localhost:5000/api/v1/predictions", new StringContent(
                JsonSerializer.Serialize(new Sentiment((string)o["data"]["text"])),
                Encoding.UTF8,"application/json"
               ));

            HttpContent content = res.Content;
            string data = await content.ReadAsStringAsync();
            
            return data;
        }
    }
}
