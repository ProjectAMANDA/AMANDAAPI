﻿using System;
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



        //[HttpGet("{sentiment}")]
        /*GetURLFromSentiment this method is being called to create a generics list of images using a LINQ that we
         * pass in the sentiment and match it against our database of cat images.
       */
        public List<string> GetURLFromSentiment(string sentiment)
        {

            float floatName = float.Parse(sentiment);
            List<string> Images = _context.Images
                                        // comparing an image list by the image sentiment to target sentiment
                                        .OrderBy(i => Math.Abs(float.Parse(i.Sentiment) - floatName))
                                        //allowing user to see url
                                        .Select(x => x.URL)
                                        // setting to list
                                        .ToList();

            return Images;
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

    }// Bottom of the v

    










}
}

    
