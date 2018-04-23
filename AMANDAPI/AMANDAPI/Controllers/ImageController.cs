using AMANDAPI.Data;
using AMANDAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;


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
        /// <param name="data">string the image suggestion will be pased upon</param>
        /// <param name="usesentiment"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        [HttpGet("{data}/{num?}")]
        public IActionResult GetUrls(string data, int num = 3 )
        {
            return  new OkObjectResult(GenerateRecs(data, num));
        }

        // pulling out the logic to make testing easier
        public Reccommendations GenerateRecs(string data, int num)
        {
            // We only have 5 images in the database
            if (num > 5)
            {
                num = 3;
            }

            //Try to parse data as a float. If succeed, put the value into sentiment and return true
            //True will get you into the validation block.
            bool usesentiment = false;
            if (float.TryParse(data, out float sentiment))
            {
                usesentiment = true; // IF the input can be parsed as a float, use sentiment
                if (sentiment < 0 || sentiment > 1)
                {
                    usesentiment = false; // if data can be parsed as a float BUT is not a valid value for sentiment
                    data = sentiment.ToString();
                }
            }
            IEnumerable<Image> reccomendations = usesentiment ?
                GetImageBySentiment(sentiment) :
                BingSearch(data).Result;

            //Repackage into single object
            Reccommendations rec = new Reccommendations()
            {
                Images = reccomendations.Take(num),
                UseSentiment = usesentiment,
                Sentiment = usesentiment ? sentiment : -1,
                KeyPhrase = usesentiment ? "" : data
            };
            return rec;
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

        /// <summary>
        /// Given a query, search bing and retrieve image results. Safe search is set to strict.
        /// </summary>
        /// <param name="searchQuery">The query for the bing search</param>
        /// <returns>a list of Image objects</returns>
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

        /// <summary>
        /// Given a target sentiment, this will query our database and provide a list of images sorted by the nearness
        /// of sentiment matching.
        /// </summary>
        /// <param name="sentiment">Target sentiment</param>
        /// <returns>sorted list of Images</returns>
        public List<Image> GetImageBySentiment(float sentiment)
        {
            // comparing an image list by the image sentiment to target sentiment
            List<Image> Images = _context.Images
                                .OrderBy(i => Math.Abs(i.Sentiment - sentiment))
                                // setting to list
                                .ToList();
            return Images;
        }
    }
}