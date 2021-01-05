using Microsoft.AspNetCore.Mvc;
using ML_Server.DataModels;
using System.Collections.Generic;
using Microsoft.ML;

namespace ML_Server.Controllers
{
    [Route("api/v2/predictions")]
    [ApiController]
    public class SentimentController : ControllerBase
    {
        public PredictionEngine<SentimentData, SentimentPrediction> PredictionEngine { get; }

        public SentimentController(PredictionEngine<SentimentData, SentimentPrediction> predictionEnginePool)
        {
            PredictionEngine = predictionEnginePool;
        }

        [HttpPost]
        public ActionResult<SentimentPrediction> Post([FromBody] SentimentData data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            SentimentPrediction predictedValue = PredictionEngine.Predict(data);
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
                SentimentPrediction predictedValue = PredictionEngine.Predict(data);
                if (predictedValue.Prediction == true) 
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

            if(probabilityPositive >= probabilityNegative)
                return Ok(new Sentiment(true,probabilityPositive/countPositive));
            else
                return Ok(new Sentiment(false, probabilityNegative / countNegative));
        }

    }
}
