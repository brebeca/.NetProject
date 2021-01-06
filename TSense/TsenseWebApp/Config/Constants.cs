using System.Configuration;

namespace TsenseWebApp.Config
{
    static class Constants
    {
        public static readonly string MultiplePredictionsUrl = ConfigurationManager.AppSettings.Get("MultiplePredictionsUrl");// "http://localhost:5000/api/v1/predictions/multiple";// 
        public static readonly string SinglePredictionUrl = ConfigurationManager.AppSettings.Get("SinglePredictionUrl");// "http://localhost:5000/api/v1/predictions";// 
        public static readonly string TweetFromLinkUrl = ConfigurationManager.AppSettings.Get("TweetFromLinkUrl");//"http://localhost:57797/twitter_api/tweet?tweetLink=";// 
        public static readonly string TweetsForUserUrl = ConfigurationManager.AppSettings.Get("TweetsForUserUrl");//"http://localhost:57797/twitter_api/tweet_all?username=";// 
        public static readonly string Probability = "probability";
        public static readonly string Prediction = "prediction";
        public static readonly int NumberOfDecimals = 2;

    }
}
