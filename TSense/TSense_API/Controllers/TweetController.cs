using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TSense_API.Models;
using System.Configuration;

namespace API.Controllers
{
    [ApiController]
    [Route("twitter_api/")]
    public class TweetController
    {
        Token token = new Token();

        [HttpGet("tweet")]
        public async Task<string> Get(string tweetLink)
        {
            using (var httpClient = new HttpClient())
            {
                string[] linkList = tweetLink.Split('/');
                int index = linkList.Length;
                string tweetId = linkList[index - 1];
                string tweetUrl = ConfigurationManager.AppSettings.Get("tweetUrl") + tweetId;
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), tweetUrl))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Bearer " + token.BearerToken);

                    var response = await httpClient.SendAsync(request);

                    return (string)JObject.Parse(response.Content.ReadAsStringAsync().Result)["data"]["text"];
                }
            }
        }

        [HttpGet("tweet_all")]
        public async Task<string> GetAll(string username)
        {
            List<string> resultArray = new List<string>();
            using (var httpClient = new HttpClient())
            {
                for (int i = 1; i <= 16; i++)
                {
                    string twitterUrl = ConfigurationManager.AppSettings.Get("twitterUrl") + 
                        username + "&exclude_replies=true&include_rts=false&count=200&page=" + i;
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), twitterUrl))
                    {
                        request.Headers.TryAddWithoutValidation("Authorization", "Bearer " + token.BearerToken);

                        var response = await httpClient.SendAsync(request);
                        var tweetsArray = JArray.Parse(response.Content.ReadAsStringAsync().Result);
                        foreach (var element in tweetsArray)
                        {
                            resultArray.Add((string)element["text"]);
                        }
                        
                    }
                }
                return JsonConvert.SerializeObject(resultArray);
            }
        }
    }
}
