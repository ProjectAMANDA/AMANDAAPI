using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AMANDAPI.Data;
using System.Net.Http;

namespace AMANDAPI.Models
{
    public class ImageController : Controller
    {
        private ImagesContext _context;

        public ImageController(ImagesContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(
                    "https://api.cognitive.microsoft.com/bing/v7.0/images/search");

                var response = client.GetAsync("[?q][&count][&offset][&mkt][&safeSearch]");
            }

            return View();
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