using System.Configuration;

namespace TSense_API.Configs
{
    static class Constants
    {
        public static readonly string TwitterUrl = ConfigurationManager.AppSettings.Get("twitterUrl");
        public static readonly string BearerToken = ConfigurationManager.AppSettings.Get("BearerToken");
        public static readonly string TweetUrl= ConfigurationManager.AppSettings.Get("tweetUrl");
        public static readonly string Text = "text";
        public static readonly string Data = "data";
        public static readonly string BackSlash = "/";
        public static readonly int NumberOfPages = 16;
        public static readonly string GetParameters = "&exclude_replies=true&include_rts=false&count=200&page=";
    }
}
