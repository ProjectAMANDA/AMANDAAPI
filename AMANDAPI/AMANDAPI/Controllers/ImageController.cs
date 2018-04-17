using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AMANDAPI.Data;
using AMANDAPI.Models;

namespace AMANDAPI.Controllers
{

    [Route("api/image")]
   
    public class ImageController : Controller
    {
        
        private readonly ImagesContext _context;

        //constructor connecting to the database
        public ImageController(ImagesContext context)
        {
            _context = context;
        }


        [HttpGet("{sentiment:float}")]
        public IActionResult Index(float sentiment)
        {

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
