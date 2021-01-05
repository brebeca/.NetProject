using Microsoft.VisualStudio.TestTools.UnitTesting;
using API.Controllers;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text.RegularExpressions;
using TsenseWebApp.Data;
using Newtonsoft.Json.Linq;
using ML_Server.Controllers;
using Microsoft.Extensions.ML;
using Microsoft.OpenApi.Models;
using ML_Server.DataModels;

namespace TwitterTests
{
    [TestClass]
    public class TwitterAPITests
    {

        [TestMethod]
        public void Get_CorrectURL_TweetString()
        {
            //Arrange
            string tweetLink = "https://twitter.com/tweeter/status/489879052157595649";
            string expected = "Stay safe. Love one another. Life is hard for everyone, so spread peace and happiness. #tweetlove";
            TweetController tweetController = new TweetController();

            //Act
            var result =  tweetController.Get(tweetLink);

            //Assert
             Assert.AreEqual(expected, result.Result);
        }

        [TestMethod]
        public void GetAll_CorrectURL_TweetsStringListFormat()
        {
             //Arrange
             string username = "tweeter";
             TweetController tweetController = new TweetController();
             Regex rx = new Regex(@"^\[.*\]$",RegexOptions.Compiled | RegexOptions.IgnoreCase);

             //Act
             var result = tweetController.GetAll(username).Result;
             MatchCollection matches = rx.Matches(result);

             //Assert
             Assert.IsInstanceOfType(result, typeof(string));
             Assert.IsTrue(matches.Count!=0);
            
        }


       /* [TestMethod]
        public void SentimentFromLink_PositiveSentiment_1()
        {
            //Arrange
            MLService mL = new MLService(new System.Net.Http.HttpClient());

            //Act
            JObject sentiment = mL.SentimentFromLink("it is good").Result;

            //Assert
            Assert.AreEqual(sentiment["prediction"], 1);
        }
       */

    }
}
