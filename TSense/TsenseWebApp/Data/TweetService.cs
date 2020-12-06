using System;
using System.Collections.Generic;
using System.Linq;
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
            string url = "api/tweet?tweetLink=" + link;
            return await httpClient.GetStringAsync(url);
        }
    }
}
