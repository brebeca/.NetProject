using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ML;
using ML_Server.DataModels;
using System;

namespace ML_Server.Controllers
{
    [Route("api/v1/predictions")]
    [ApiController]
    public class SentimentSController : ControllerBase
    {
        public PredictionEnginePool<SentimentData, SentimentPrediction> PredictionEnginePool { get; }
        public SentimentSController(PredictionEnginePool<SentimentData, SentimentPrediction> predictionEnginePool)
        {
            PredictionEnginePool = predictionEnginePool;
        }

        [HttpPost]
        public ActionResult<SentimentPrediction> Post([FromBody] SentimentData data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            SentimentPrediction predictedValue = PredictionEnginePool.Predict(modelName: "SentimentAnalisys", example: data);
           // string sentiment = Convert.ToBoolean(predictedValue.Prediction) ? "Positive" : "Negative";
            return Ok(predictedValue);
        }

    }
}
