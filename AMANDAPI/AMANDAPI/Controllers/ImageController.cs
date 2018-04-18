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

/* This is the controller used to Pull images from ImagesContext DB;
 * There is also a controller used to pull from Microsoft Analytics API and Bing Search API */

namespace AMANDAPI.Controllers
{   

    [Route("api/image")]
    public class ImageController : Controller
    {
        //Setting ImagesContext DB to ready only
        private readonly ImagesContext _context;

        // Kevin's access key
        const string accessKey = "26f5d2c5dad8494b867de53f057850c1";

        //constructor connecting to the database as _context
        public ImageController(ImagesContext context)
        {
            _context = context;
        }

        // "[?q][&count][&offset][&mkt][&safeSearch]"

        public async Task<BingJson> BingSearch(string searchQuery)
        {

            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", accessKey);

            // Request parameters
            queryString["q"] = searchQuery;
            queryString["count"] = "15";
            queryString["offset"] = "0";
            queryString["mkt"] = "en-us";
            queryString["safeSearch"] = "Strict";
            string uri = "https://api.cognitive.microsoft.com/bing/v7.0/images/search?" + queryString;

            var response = await client.GetAsync(uri);
            string responseString = await response.Content.ReadAsStringAsync();
            if (responseString != null)
            {
                return JsonConvert.DeserializeObject<BingJson>(responseString);
            }
            return JsonConvert.DeserializeObject<BingJson>(responseString);
        }
        
        /* Get Method to pass sentiment to Microsoft analytics
         *  User text -> [analytics] -> [context Database] */
         
        [HttpGet]
        public IEnumerable<string> GetUrls([FromHeader] string text)
        {
            Analytics analysis = Analyze(text);
            return GetURLFromSentiment(analysis.Sentiment);
        }

        //[HttpGet("{sentiment}")]

        /*GetURLFromSentiment this method is being called to create a generics list of images using a LINQ that we
          pass in the sentiment and match it against our context database of cat images.*/
       
        public List<string> GetURLFromSentiment(float sentiment)
        {
            List<string> Images = _context.Images
                                        
                                        .OrderBy(i => Math.Abs(float.Parse(i.Sentiment) - sentiment))
                                        
                                        .Select(x => x.URL)
                                      
                                        .ToList();
            return Images;
        }

        /*Delete method for finding images by Image ID*/

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var ImageDelete = _context.Images
                        .FirstOrDefault(t => t.Id == id);
            if (ImageDelete == null)
            {
                return NotFound();
            }

            _context.Images.Remove(ImageDelete);
            _context.SaveChanges();
            return new NoContentResult();
        }

        /* Post method for adding the images to the body of our front-end*/

        [HttpPost]
        public IActionResult Create([FromBody]Image image)
        {
            if (image == null)
            {
                return BadRequest();
            }
            _context.Images.Add(image);
            _context.SaveChanges();
            return View();
        }

        [HttpPut]
        public IActionResult Edit()
        {
            return View();
        }

        /* This method uses the Microsoft cognitive services */

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