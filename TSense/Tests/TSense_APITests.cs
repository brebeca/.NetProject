using Microsoft.VisualStudio.TestTools.UnitTesting;
using API.Controllers;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TwitterTests
{
    [TestClass]
    public class TwitterAPITests
    {

        [TestMethod]
        public void TestGetMesaj()
        {
            //Arrange
            string tweetLink = "https://twitter.com/realDonaldTrump/status/1336730906107768842";
            string expected = "{\"data\":{\"id\":\"1336730906107768842\",\"text\":\"If somebody cheated in the Election, which the Democrats did, why wouldn't the Election be immediately overturned? How can a Country be run like this?\"}}";
            TweetController t = new TweetController();
            //Act
            Task<string> actual = t.Get(tweetLink);
            Trace.WriteLine(t.Get(tweetLink));
            //Assert
            Assert.AreEqual(expected, actual, "Mesaj Twitter gresit");
        }
    }
}
