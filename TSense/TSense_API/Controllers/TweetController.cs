using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TSense_API.Models;
using System.Configuration;
using TSense_API.Configs;

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
                string[] linkList = tweetLink.Split(Constants.BackSlash);
                string tweetId = linkList[linkList.Length - 1];
                string tweetUrl = Constants.TweetUrl + tweetId;

                using (var request = new HttpRequestMessage(new HttpMethod("GET"), tweetUrl))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Bearer " + token.BearerToken);

                    var response = await httpClient.SendAsync(request);

                    return (string)JObject.Parse(response.Content.ReadAsStringAsync().Result)[Constants.Data][Constants.Text];
                }
            }
        }

        [HttpGet("tweet_all")]
        public async Task<string> GetAll(string username)
        {
            List<string> resultArray = new List<string>();
            using (var httpClient = new HttpClient())
            {
                
                for (int pageNumber = 1; pageNumber <= Constants.NumberOfPages; pageNumber++)
                {
                    string twitterUrl = Constants.TwitterUrl + 
                        username +
                        Constants.GetParameters +
                        pageNumber;

                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), twitterUrl))
                    {
                        request.Headers.TryAddWithoutValidation("Authorization", "Bearer " + token.BearerToken);

                        var response = await httpClient.SendAsync(request);
                        var tweetsArray = JArray.Parse(response.Content.ReadAsStringAsync().Result);
                        foreach (var element in tweetsArray)
                        {
                            resultArray.Add((string)element[Constants.Text]);
                        }
                        
                    }
                }
                return JsonConvert.SerializeObject(resultArray);
            }
        }
    }
}
