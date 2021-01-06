using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TSense_API.Models;
using System.Configuration;
using TSense_API.Configs;
using System;
using System.Net;
using System.Web.Http;
using System.Text.RegularExpressions;

namespace API.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("twitter_api/")]
    public class TweetController : ControllerBase
    {
        Token token = new Token();

        [Microsoft.AspNetCore.Mvc.HttpGet("tweet")]
        public async Task<IActionResult> Get(string tweetLink)
        {
            string pattern = @"https://twitter.com/[a-zA-z0-9]+/status/[0-9]+";
            Regex re = new Regex(pattern);
            if (!re.IsMatch(tweetLink)) 
                return NotFound("Wrong link");
            using (var httpClient = new HttpClient())
            {
                string[] linkList = tweetLink.Split(Constants.BackSlash);
                string tweetId = linkList[linkList.Length - 1];
                string tweetUrl = Constants.TweetUrl + tweetId;

                using (var request = new HttpRequestMessage(new HttpMethod("GET"), tweetUrl))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Bearer " + token.BearerToken);

                    var response = await httpClient.SendAsync(request);
                    try
                    {
                        var tweetText = (string)JObject.Parse(response.Content.ReadAsStringAsync().Result)[Constants.Data][Constants.Text];
                        return Ok(tweetText);
                    }
                    catch(Exception)
                    {
                        return NotFound("Wrong link");
                    }
                }
            }
        }

        [Microsoft.AspNetCore.Mvc.HttpGet("tweet_all")]
        public async Task<IActionResult> GetAll(string username)
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
                        try
                        {
                            var tweetsArray = JArray.Parse(response.Content.ReadAsStringAsync().Result);
                            foreach (var element in tweetsArray)
                            {
                                resultArray.Add((string)element[Constants.Text]);
                            }
                        }
                        catch(Exception)
                        {
                            return NotFound("Wrong username");
                        }
                        
                    }
                }
                return Ok(JsonConvert.SerializeObject(resultArray));
            }
        }
    }
}
