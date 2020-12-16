using System.Configuration;

namespace TSense_API.Models
{
    public class Token
    {
        public readonly string BearerToken;
        public Token()
        {
            BearerToken = ConfigurationManager.AppSettings.Get("BearerToken");
        }
    }
}
