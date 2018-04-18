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
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;


namespace AMANDAPI.Controllers
{
    [Route("api/image")]
    public class ImageController : Controller
    {
        private readonly ImagesContext _context;
        // Bing API key
        const string accessKey = "26f5d2c5dad8494b867de53f057850c1";

        //constructor connecting to the database
        public ImageController(ImagesContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<string>> BingSearch(string searchQuery)
        {
            var client = new HttpClient();
            //Create our query string dictionary starting with an empty string
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Set the authentication headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", accessKey);

            // Request parameters
            queryString["q"] = searchQuery;
            queryString["count"] = "15";
            queryString["offset"] = "0";
            queryString["mkt"] = "en-us";
            queryString["safeSearch"] = "Strict";
            // Build the query string
            string uri = "https://api.cognitive.microsoft.com/bing/v7.0/images/search?" + queryString;
            // Make the call to Bing Image Search API
            var response = await client.GetAsync(uri);
            // Pull a string out of the response body
            string responseString = await response.Content.ReadAsStringAsync();
            // CHeck if we got something back from Bing
            if (responseString != null)
            {
                //Parse just the JSON we care about into a JObject
                var data = JObject.Parse(responseString)["value"];
                //Pull the list of thumbnail URLs
                IEnumerable<string> valueList = from JObject n 
                                                in data
                                                select n["thumbnailUrl"].ToString();

                return valueList.ToList();
            }

            //If Bing did not return a result send back an empty list
            return new List<string>();
        }

        [HttpGet("{data}/{usesentiment?}/{num?}")]
        public IEnumerable<string> GetUrls(string data, string usesentiment = "true", string num = "3" )
        {
            int numRecs;
            try
            {
                numRecs = int.Parse(num);
                if (numRecs > 6)
                    throw new Exception();
            }
            catch
            {
                numRecs = 3;
            }
            List<string> reccomendations = usesentiment == "true" ? GetURLFromSentiment(float.Parse(data)) : new List<string> { "brent made a mistake", "blame it on brent"};//Bing search results will go here
            return reccomendations.Take(numRecs);
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
    }

}

    
