using System.Configuration;

namespace TsenseWebApp.Config
{
    static class Constants
    {
        public static readonly string MultiplePredictionsUrl = ConfigurationManager.AppSettings.Get("MultiplePredictionsUrl");
        public static readonly string SinglePredictionUrl = ConfigurationManager.AppSettings.Get("SinglePredictionUrl");
        public static readonly string TweetFromLinkUrl = ConfigurationManager.AppSettings.Get("TweetFromLinkUrl");
        public static readonly string TweetsForUserUrl = ConfigurationManager.AppSettings.Get("TweetsForUserUrl");
        public static readonly string Probability = "probability";
        public static readonly string Prediction = "prediction";
        public static readonly int NumberOfDecimals = 2;

    }
}
