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
using Microsoft.Extensions.Configuration;


namespace AMANDAPI.Controllers
{
    [Route("api/image")]
    public class ImageController : Controller
    {
        private readonly ImagesContext _context;
        private readonly IConfiguration Configuration;

        //constructor connecting to the database
        public ImageController(ImagesContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }
        
        /// <summary>
        /// Main meat of the app
        /// </summary>
        /// <param name="data"></param>
        /// <param name="usesentiment"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        [HttpGet("{data}/{num?}")]
        public IActionResult GetUrls(string data, int num = 3 )
        {
            return  new OkObjectResult(GenerateRecs(data, num));
        }

        public Reccommendations GenerateRecs(string data, int num)
        {
            bool usesentiment = false;
            try
            {
                if (num > 6)
                    throw new Exception();
            }
            catch
            {
                num = 3;
            }

            //Try to parse data as a float. If succeed, put the value into sentiment and return true
            //True will get you into the validation block.
            if (float.TryParse(data, out float sentiment))
            {
                usesentiment = true;
                if (sentiment < 0 || sentiment > 1)
                {
                    usesentiment = false;
                    data = sentiment.ToString();
                }
            }
            IEnumerable<Image> reccomendations = usesentiment ?
                GetImageBySentiment(sentiment) :
                BingSearch(data).Result;
            Reccommendations rec = new Reccommendations()
            {
                Images = reccomendations.Take(num),
                UseSentiment = usesentiment,
                Sentiment = usesentiment ? sentiment : -1,
                KeyPhrase = usesentiment ? "" : data
            };
            return rec;
        }

        public List<Image> GetImageBySentiment(float sentiment)
        {
            // comparing an image list by the image sentiment to target sentiment
            List<Image> Images = _context.Images
                                .OrderBy(i => Math.Abs(i.Sentiment - sentiment))
                                // setting to list
                                .ToList();
            return Images;
        }

        /// <summary>
        /// Removes items from database
        /// </summary>
        /// <param name="id">id of image to remove</param>
        /// <returns>does not matter.</returns>
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

        /// <summary>
        /// This was used to populate the database.
        /// </summary>
        /// <param name="image">Image to add to the database</param>
        /// <returns>does not matter.</returns>
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

        public async Task<IEnumerable<Image>> BingSearch(string searchQuery)
        {
            var client = new HttpClient();
            //Create our query string dictionary starting with an empty string
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Set the authentication headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", 
                Configuration["myBingAPIKey"]);

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
                IEnumerable<Image> valueList = from JObject n
                                                in data
                                               select new Image(n["thumbnailUrl"].ToString());
                return valueList.ToList();
            }
            //If Bing did not return a result send back an empty list
            return new List<Image>();
        }
    }
}