using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AMANDAPI.Data;
using System.Net.Http;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Web;
using AMANDAPI.Models;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using Microsoft.AspNetCore.Routing;

namespace AMANDAPI.Controllers
{
    /// <summary>
    /// This controller allows user to get analyze text input for and redirect to image selection.
    /// </summary>

    [Route("api/analytics")]

    public class AnalyticsController : Controller
    {

        [HttpGet("{usesentiment}")]
        public IActionResult GetText(string usesentiment, [FromHeader] string text)
        {
            Analytics analysis = Analyze(text);

            string data = analysis.Keywords.FirstOrDefault();

            if (usesentiment == "true")
            {
                data = analysis.Sentiment.ToString();
            }

            return RedirectToAction("GetURLs", "ImageController", new RouteValueDictionary { { "data", data }, {"usesentiment", usesentiment}, { "num", "3"} });

        }




        /* Method to use Microsoft Cognitive services*/

        private Analytics Analyze(string body)
        {
            // Create a client.
            ITextAnalyticsAPI client = new TextAnalyticsAPI();
            client.AzureRegion = AzureRegions.Westcentralus;
            //user key is Brent 
            client.SubscriptionKey = "d8646ffcf51c4855a5d348e682b270c0";

            List<string> keyPhrases = new List<string>();//output
            float sentiment = 0;

            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Getting key-phrases

            KeyPhraseBatchResult result2 = client.KeyPhrases(
                    new MultiLanguageBatchInput(
                        new List<MultiLanguageInput>()
                        {
                          new MultiLanguageInput("en", "3", body),
                        }));


            // Printing key-phrases
            foreach (var document in result2.Documents)
            {

                foreach (string keyphrase in document.KeyPhrases)
                {
                    keyPhrases.Add(keyphrase);
                }
            }

            // Extracting sentiment

            SentimentBatchResult result3 = client.Sentiment(
                    new MultiLanguageBatchInput(
                        new List<MultiLanguageInput>()
                        {
                          new MultiLanguageInput("en", "0", body)
                        }));

            // Printing sentiment results
            foreach (var document in result3.Documents)
            {
                sentiment = (float)document.Score;
            }

            return new Models.Analytics() { Keywords = keyPhrases, Sentiment = sentiment };
        }

    }
}
