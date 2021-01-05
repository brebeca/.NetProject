using Microsoft.VisualStudio.TestTools.UnitTesting;
using API.Controllers;
using Microsoft.Extensions.ML;
using Moq;


namespace UnitTests
{
    [TestClass]
    public class TweeterServerUnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            var controller = new TweetController();
            string link = "https://twitter.com/_8bitwizard/status/1147479700236845057";

            //Act
            var result = controller.Get(link);

            //Asert
            Assert.

        }
    }
}
