using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/tweet")]
    public class TweetController
    {
        [HttpGet]
        public async Task<string> GetTweetTextAsync([FromBody] string link)
        {
            using (var httpClient = new HttpClient())
            {
                string[] linkList = link.Split('/');
                int index = linkList.Length;
                string tweetId = linkList[index - 1];
                string tweetUrl = "https://api.twitter.com/2/tweets/" + tweetId;
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), tweetUrl))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Bearer AAAAAAAAAAAAAAAAAAAAAFO6JwEAAAAAx90c8Q%2BhBkRXik%2BwT7Xs%2B8RSaFE%3DmARWn3au4pIFq62bWaCDXMNGT1TGfHfxCA3CqrnOURGadrCUhG");

                    var response = await httpClient.SendAsync(request);

                    return response.Content.ReadAsStringAsync().Result;
                }
            }
        }
    }
}
