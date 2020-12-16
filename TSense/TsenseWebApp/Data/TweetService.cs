using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

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
            string url = "twitter_api/tweet?tweetLink=" + link;
            return await httpClient.GetStringAsync(url);
        }

        public async Task<List<string>> GetTweetsFromUser(string username)
        {
            string url = "twitter_api/tweet_all?username=" + username;
            return JsonConvert.DeserializeObject<List<string>>( await httpClient.GetStringAsync(url));
        }
    }
}
