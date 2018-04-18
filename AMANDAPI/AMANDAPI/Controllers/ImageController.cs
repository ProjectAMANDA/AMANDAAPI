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

namespace AMANDAPI.Controllers
{
    [Route("api/image")]
    public class ImageController : Controller
    {

        private readonly ImagesContext _context;

        const string accessKey = "26f5d2c5dad8494b867de53f057850c1";

        //constructor connecting to the database
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


        //[HttpGet]
        //public IEnumerable<Image> GetAllImagesInDb()
        //{
        //    return _context.Images.ToList();

        //}

        [HttpGet("{keyword}"})]
        public IEnumerable<string> GetUrls([FromHeader] string text, [FromHeader] string sentiment = "true", [FromHeader] string numRecs = "3" )
        {
            int num;
            try
            {
                num = int.Parse(numRecs);
                if (num > 6)
                    throw new Exception();
            }
            catch
            {
                num = 3;
            }
            Analytics analysis = Analyze(text);
            List<string> reccomendations = sentiment == "true" ? GetURLFromSentiment(analysis.Sentiment) : new List<string> { "brent made a mistake", "blame it on brent"};//Bing search results will go here
            return reccomendations.Take(num);
        }


        //[HttpGet("{sentiment}")]
        /*GetURLFromSentiment this method is being called to create a generics list of images using a LINQ that we
         * pass in the sentiment and match it against our database of cat images.
       */
        public List<string> GetURLFromSentiment(float sentiment)
        {
            List<string> Images = _context.Images
                                        // comparing an image list by the image sentiment to target sentiment
                                        .OrderBy(i => Math.Abs(float.Parse(i.Sentiment) - sentiment))
                                        //allowing user to see url
                                        .Select(x => x.URL)
                                        // setting to list
                                        .ToList();
            return Images;
        }


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

        // I'm assuming this model will eventually need
        // to be moved into the images controller. If so, change to private
        private Analytics Analyze(string body)
        {
            // Create a client.
            ITextAnalyticsAPI client = new TextAnalyticsAPI();
            client.AzureRegion = AzureRegions.Westcentralus;
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


            // keyphrases
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

            // sentiment results
            foreach (var document in result3.Documents)
            {
                sentiment = (float)document.Score;
            }

            return new Analytics() { Keywords = keyPhrases, Sentiment = sentiment };
        }
    }// Bottom of the v
}

    
