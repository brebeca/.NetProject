using TSense_API.Configs;

namespace TSense_API.Models
{
    public class Token
    {
        public readonly string BearerToken;
        public Token()
        {
            BearerToken =Constants.BearerToken ;
        }
    }
}
