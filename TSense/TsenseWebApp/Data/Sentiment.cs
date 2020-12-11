namespace TsenseWebApp.Data
{
    public class Sentiment
    {
        public string SentimentText { get; set; }
        public Sentiment(string sentimentText)
        {
            SentimentText = sentimentText;
        }

    }
}
