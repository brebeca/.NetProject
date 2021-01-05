using System.Configuration;

namespace TSense_API.Configs
{
    static class Constants
    {
        public static readonly string TwitterUrl = "https://api.twitter.com/1.1/statuses/user_timeline.json?screen_name=";// ConfigurationManager.AppSettings.Get("twitterUrl");
        public static readonly string BearerToken = "AAAAAAAAAAAAAAAAAAAAAFO6JwEAAAAAx90c8Q%2BhBkRXik%2BwT7Xs%2B8RSaFE%3DmARWn3au4pIFq62bWaCDXMNGT1TGfHfxCA3CqrnOURGadrCUhG";// ConfigurationManager.AppSettings.Get("BearerToken");
        public static readonly string TweetUrl= "https://api.twitter.com/2/tweets/";//ConfigurationManager.AppSettings.Get("tweetUrl");
        public static readonly string Text = "text";
        public static readonly string Data = "data";
        public static readonly string BackSlash = "/";
        public static readonly int NumberOfPages = 16;
        public static readonly string GetParameters = "&exclude_replies=true&include_rts=false&count=200&page=";
    }
}
