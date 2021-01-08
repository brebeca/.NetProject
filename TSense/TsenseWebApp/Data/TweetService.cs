using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TsenseWebApp.Config;

namespace TsenseWebApp.Data
{
    public class TweetService
    {
        private readonly HttpClient httpClient;

        public TweetService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<string> GetTextFromTweet(string link)
        {
            string url = Constants.TweetFromLinkUrl + link;
            var response = await httpClient.GetAsync(url);
            if (response.StatusCode == (System.Net.HttpStatusCode)404)
                return "Wrong link";
            return await httpClient.GetStringAsync(url);
        }

        public async Task<List<string>> GetTweetsFromUser(string username)
        {
            string url = Constants.TweetsForUserUrl + username;
            var response = await httpClient.GetAsync(url);
            if (response.StatusCode == (System.Net.HttpStatusCode)404)
                return new List<string>() { "Wrong username" };
            return JsonConvert.DeserializeObject<List<string>>( await httpClient.GetStringAsync(url));
        }
    }
}
