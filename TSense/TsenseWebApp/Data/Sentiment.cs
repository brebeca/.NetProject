using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
