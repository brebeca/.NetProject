using Microsoft.VisualStudio.TestTools.UnitTesting;
using API.Controllers;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text.RegularExpressions;
using TsenseWebApp.Data;
using Newtonsoft.Json.Linq;

namespace TwitterTests
{
    [TestClass]
    class WebAppTests
    {

        [TestMethod]
        public void SentimentFromLink_PositiveSentiment_1()
        {
            //Arrange
            MLService mL = new MLService(new System.Net.Http.HttpClient());

            //Act
            JObject sentiment = mL.SentimentFromLink("it is good").Result;

            //Assert
            Assert.AreEqual(sentiment["prediction"], 1);
        }
    }
}
