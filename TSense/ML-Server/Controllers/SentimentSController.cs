using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.ML;
using ML_Server.DataModels;

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
        public ActionResult<string> Post([FromBody] SentimentData data)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            SentimentPrediction predictedValue = PredictionEnginePool.Predict(modelName: "SentimentAnalisys", example: data);
            string sentiment = Convert.ToBoolean(predictedValue.Prediction) ? "Positive" : "Negative";
            return Ok(sentiment);
        }
       
    }
}
