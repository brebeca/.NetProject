using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ML;
using ML_Server.DataModels;
using System.Configuration;
using System.Collections.Generic;

namespace ML_Server.Prediction
{
    public static class Prediction
    {
        public static SentimentPrediction getPrediction(SentimentData data , PredictionEnginePool<SentimentData, SentimentPrediction> PredictionEnginePool)
        {
             return PredictionEnginePool.Predict(modelName: ConfigurationManager.AppSettings.Get("modelName"), example: data);
        }
    }
}
