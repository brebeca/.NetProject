using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ML_Server.DataModels
{
    public class Sentiment
    {
        public bool Prediction { get; set; }
        public float Probability { get; set; }

        public Sentiment(bool prediction, float probability)
        {
            Prediction = prediction;
            Probability = probability;
        }
    }
}
