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
