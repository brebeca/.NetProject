using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ML;
using ML_Server.DataModels;
using System.Configuration;
using System.Collections.Generic;

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

            SentimentPrediction predictedValue = PredictionEnginePool.Predict(modelName: ConfigurationManager.AppSettings.Get("modelName"), example: data);
            return Ok(predictedValue);
        }

        [HttpPost("multiple")]
        public ActionResult<float> PostMultiple([FromBody] ICollection<SentimentData> texts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            float probabilityPositive = 0;
            float probabilityNegative = 0;
            int countPositive = 0;
            int countNegative = 0;
            foreach (SentimentData data in texts)
            {
                SentimentPrediction predictedValue = PredictionEnginePool.Predict(modelName: ConfigurationManager.AppSettings.Get("modelName"), example: data);
                if (predictedValue.Sentiment == true) 
                {
                    probabilityPositive += predictedValue.Probability;
                    countPositive ++ ;
                }
                else 
                {
                    probabilityNegative += predictedValue.Probability;
                    countNegative ++;
                }
                    
            }

            if(probabilityNegative<probabilityPositive)
                return Ok(new Sentiment(true,probabilityPositive/countPositive));
            else
                return Ok(new Sentiment(false, probabilityNegative / countNegative));
        }

    }
}
