using System.Configuration;

namespace TsenseWebApp.Config
{
    static class Constants
    {
        public static readonly string MultiplePredictionsUrl = "http://localhost:5000/api/v1/predictions/multiple";// ConfigurationManager.AppSettings.Get("MultiplePredictionsUrl");
        public static readonly string SinglePredictionUrl = "http://localhost:5000/api/v1/predictions";// ConfigurationManager.AppSettings.Get("SinglePredictionUrl");
        public static readonly string TweetFromLinkUrl = "http://localhost:57797/twitter_api/tweet?tweetLink=";// ConfigurationManager.AppSettings.Get("TweetFromLinkUrl");
        public static readonly string TweetsForUserUrl = "http://localhost:57797/twitter_api/tweet_all?username=";// ConfigurationManager.AppSettings.Get("TweetsForUserUrl");
        public static readonly string Probability = "probability";
        public static readonly string Prediction = "prediction";
        public static readonly int NumberOfDecimals = 2;

    }
}
