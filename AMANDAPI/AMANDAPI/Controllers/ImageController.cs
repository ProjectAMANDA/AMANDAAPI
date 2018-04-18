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

        [HttpPost]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPut]
        public IActionResult Edit()
        {
            return View();
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            return View();
        }

    }

}
