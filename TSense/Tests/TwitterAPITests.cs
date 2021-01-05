using Microsoft.VisualStudio.TestTools.UnitTesting;
using API.Controllers;
using System.Text.RegularExpressions;
using ML_Server.Controllers;
using ML_Server.DataModels;
using Microsoft.ML;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TsenseWebApp.Data;
using Newtonsoft.Json.Linq;

namespace TwitterTests
{
    [TestClass]
    public class TwitterAPITests
    {

        [TestMethod]
        public void Get_CorrectLink_TweetString()
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
        public void GetAll_CorrectLink_TweetsStringListFormat()
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

        [TestMethod]
        public void Post_positiveText_True()
        {
            //Arrange
            MLContext mlContext = new MLContext();
            DataViewSchema predictionPipelineSchema;
            PredictionEngine<SentimentData, SentimentPrediction> predictionEngine = mlContext
                .Model.CreatePredictionEngine<SentimentData, SentimentPrediction>(mlContext
                .Model.Load("C:/Users/iRebeca/facultate/.NET/.NetProject/TSense/Tests/MlModels/sentiment_model.zip", out predictionPipelineSchema));
            SentimentController sentiment = new SentimentController(predictionEngine);
            var sentimentData = new SentimentData
            {
                SentimentText = "It was great"
            };

            //Act
            var result = (OkObjectResult)sentiment.Post(sentimentData).Result;
            var prediction = (SentimentPrediction)result.Value;

            //Assert
            Assert.IsInstanceOfType(result.Value, typeof(SentimentPrediction));
            Assert.AreEqual(true, prediction.Prediction);
        }

        [TestMethod]
        public void Post_negativeText_False()
        {
            //Arrange
            MLContext mlContext = new MLContext();
            DataViewSchema predictionPipelineSchema;
            ITransformer predictionPipeline = mlContext
                .Model.Load("C:/Users/iRebeca/facultate/.NET/.NetProject/TSense/Tests/MlModels/sentiment_model.zip", out predictionPipelineSchema);
            PredictionEngine<SentimentData, SentimentPrediction> predictionEngine = mlContext
                .Model.CreatePredictionEngine<SentimentData, SentimentPrediction>(predictionPipeline);
            SentimentController sentiment = new SentimentController(predictionEngine);
            SentimentData sentimentData = new SentimentData();
            sentimentData.SentimentText = "It was bad";

            //Act
            var result = (OkObjectResult)sentiment.Post(sentimentData).Result;
            var prediction = (SentimentPrediction)result.Value;

            //Assert
            Assert.IsInstanceOfType(result.Value, typeof(SentimentPrediction));
            Assert.AreEqual(prediction.Prediction, false);
        }

        [TestMethod]
        public void PostMultiple_positiveText_True()
        {
            //Arrange
            MLContext mlContext = new MLContext();
            DataViewSchema predictionPipelineSchema;
            PredictionEngine<SentimentData, SentimentPrediction> predictionEngine = mlContext
                 .Model.CreatePredictionEngine<SentimentData, SentimentPrediction>(mlContext
                 .Model.Load("C:/Users/iRebeca/facultate/.NET/.NetProject/TSense/Tests/MlModels/sentiment_model.zip", out predictionPipelineSchema));
            SentimentController sentiment = new SentimentController(predictionEngine);
            SentimentData sentimentData = new SentimentData();
            sentimentData.SentimentText = "It was great";
            List<SentimentData> sentiments = new List<SentimentData>() { sentimentData };

            //Act
            var result = (OkObjectResult)sentiment.PostMultiple(sentiments).Result;
            var prediction = (ML_Server.DataModels.Sentiment)result.Value;

            //Assert
            Assert.IsInstanceOfType(result.Value, typeof(ML_Server.DataModels.Sentiment));
            Assert.AreEqual( true, prediction.Prediction, prediction.Probability.ToString());
        }

        [TestMethod]
        public void PostMultiple_negativeTexts_False()
        {
            //Arrange
            MLContext mlContext = new MLContext();
            DataViewSchema predictionPipelineSchema;
            PredictionEngine<SentimentData, SentimentPrediction> predictionEngine = mlContext
                 .Model.CreatePredictionEngine<SentimentData, SentimentPrediction>(mlContext
                 .Model.Load("C:/Users/iRebeca/facultate/.NET/.NetProject/TSense/Tests/MlModels/sentiment_model.zip", out predictionPipelineSchema));
            SentimentController sentiment = new SentimentController(predictionEngine);
            SentimentData sentimentData = new SentimentData();
            sentimentData.SentimentText = "It was not great";
            List<SentimentData> sentiments = new List<SentimentData>() { sentimentData };



            //Act
            var result = (OkObjectResult)sentiment.PostMultiple(sentiments).Result;
            var prediction = (ML_Server.DataModels.Sentiment)result.Value;

            //Assert
            Assert.IsInstanceOfType(result.Value, typeof(ML_Server.DataModels.Sentiment));
            Assert.AreEqual(false, prediction.Prediction, prediction.Probability.ToString());
        }

        [TestMethod]
         public async System.Threading.Tasks.Task SentimentFromLink_PositiveSentiment_True()
         {
             //Arrange
             MLService mL = new MLService(new System.Net.Http.HttpClient());
            string positiveText = "it is good";

            //Act
            var sentiment = await mL.SentimentFromLink(positiveText);

             //Assert
            Assert.AreEqual(true, (bool)(sentiment)["prediction"], sentiment.ToString());
         }

        [TestMethod]
        public async System.Threading.Tasks.Task SentimentFromLink_NegativeSentiment_False()
        {
            //Arrange
            MLService mL = new MLService(new System.Net.Http.HttpClient());
            string negativeText = "it is not good";

            //Act
            var sentiment = await mL.SentimentFromLink(negativeText);

            //Assert
            Assert.AreEqual(false, (bool)(sentiment)["prediction"], sentiment.ToString());
        }

        [TestMethod]
        public async System.Threading.Tasks.Task SentimentFromMultiple_NegativeSentiment_False()
        {
            //Arrange
            MLService mL = new MLService(new System.Net.Http.HttpClient());
            List<string> negativeTexts = new List<string>() { "not good"};

            //Act
            var sentiment = await mL.SentimentFromMultiple(negativeTexts);

            //Assert
            Assert.AreEqual(false, (bool)(sentiment)["prediction"], sentiment.ToString());
        }

        [TestMethod]
        public async System.Threading.Tasks.Task SentimentFromMultiple_PositiveSentiment_True()
        {
            //Arrange
            MLService mL = new MLService(new System.Net.Http.HttpClient());
            List<string> negativeTexts = new List<string>() { "it is  good" };

            //Act
            var sentiment = await mL.SentimentFromMultiple(negativeTexts);

            //Assert
            Assert.AreEqual(true, (bool)(sentiment)["prediction"], sentiment.ToString());
        }

        [TestMethod]
        public async System.Threading.Tasks.Task SentimentFromText_PositiveSentiment_True()
        {
            //Arrange
            MLService mL = new MLService(new System.Net.Http.HttpClient());
            string positiveText = "it is good";

            //Act
            var sentiment = await mL.SentimentFromText(positiveText);

            //Assert
            Assert.AreEqual(true, (bool)(sentiment)["prediction"], sentiment.ToString());
        }

        [TestMethod]
        public async System.Threading.Tasks.Task SentimentFromtext_NegativeSentiment_False()
        {
            //Arrange
            MLService mL = new MLService(new System.Net.Http.HttpClient());
            string negativeText = "it is not good";

            //Act
            var sentiment = await mL.SentimentFromText(negativeText);

            //Assert
            Assert.AreEqual(false, (bool)(sentiment)["prediction"], sentiment.ToString());
        }

        [TestMethod]
        public async System.Threading.Tasks.Task GetTextFromTweet_CorrectLink_TweetText()
        {
            //Arrange
            TweetService tweetService = new TweetService(new System.Net.Http.HttpClient());
            string tweetLink = "https://twitter.com/tweeter/status/489879052157595649";
            string expected = "Stay safe. Love one another. Life is hard for everyone, so spread peace and happiness. #tweetlove";


            //Act
            var text = await tweetService.GetTextFromTweet(tweetLink);

            //Assert
            Assert.AreEqual(expected, text);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task GetTweetsFromUser_ExistingUser_TweetsText()
        {
            //Arrange
            TweetService tweetService = new TweetService(new System.Net.Http.HttpClient());
            string username = "twitter";

            //Act
            var result = await tweetService.GetTweetsFromUser(username);
            bool notEmpty = result.Count != 0;

            //Assert
            Assert.IsInstanceOfType(result, typeof(List<string>));
            Assert.IsTrue(notEmpty);
        }

    }
}
