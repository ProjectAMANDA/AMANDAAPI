﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using AMANDAPI.Models;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;

namespace AMANDAPI.Controllers
{
    /// <summary>
    /// This controller allows user to get analyze text input for and redirect to image selection.
    /// </summary>

    [Route("api/analytics")]

    public class AnalyticsController : Controller
    {
        private readonly IConfiguration Configuration;

        //Pull in configure API for user secrets
        public AnalyticsController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// This action will analyze text for sentiment and keywords and then redirect to our image suggestion endpoint.
        /// If you don't specify to use sentiment or number of pictures to receive, it defaults to 3.
        /// </summary>
        /// <param name="text">Text to be analyzed</param>
        /// <param name="usesentiment">Optional. "true" to use sentiment. Anything else will use keywords. Defaults to true</param>
        /// <param name="num">Optional. number of images to be retrieved. Defaults to 3.</param>
        /// <returns>redirect to get URL from image suggestion.</returns>
        [HttpGet("{usesentiment?}/{num?}")]
        public IActionResult GetText([FromHeader]string text, string usesentiment = "true", string num = "3")
        {
            //The actual querying of cognitive analytics is called here
            Analytics analysis = Analyze(text);

            //Repackage the results to sent to Image endpoint where actual image suggestion takes place.
            string data = analysis.Keywords.FirstOrDefault();

            if (usesentiment == "true")
            {
                data = analysis.Sentiment.ToString();
            }
            
            // perform Image suggestion
            return RedirectToAction("GetUrls", "Image", new { data, num });
        }

        /// <summary>
        /// Query the Azure congnitive analytics API for text analysis. This utilizes a nuget package and is largely the example
        /// code they provided with a few tweaks.
        /// </summary>
        /// <param name="body">Text body to be analyzed</param>
        /// <returns>an instance of the Analytics class that has the relevant parts of the analysis repackaged for ease of use</returns>
        public Analytics Analyze(string body)//Note: this was private, but that made testing the controller difficult
        {
            // Create a client.
            ITextAnalyticsAPI client = new TextAnalyticsAPI();
            client.AzureRegion = AzureRegions.Westcentralus;
           
            client.SubscriptionKey = Configuration["textAPIKey"];

            // initialize output vars
            List<string> keyPhrases = new List<string>();
            float sentiment = 0;

            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Getting key-phrases
            // this is taken almost word for word from the example code in the docs for the API
            KeyPhraseBatchResult result2 = client.KeyPhrases(
                    new MultiLanguageBatchInput(
                        new List<MultiLanguageInput>()
                        {
                          new MultiLanguageInput("en", "3", body),
                        }));

            // Unpack key phrases into list
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

            // Unpack sentiment results
            foreach (var document in result3.Documents)
            {
                sentiment = (float)document.Score;
            }

            // Repack analysis into analytics instance for convenience of use.
            return new Analytics() { Keywords = keyPhrases, Sentiment = sentiment };
        }
    }
}
