using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AMANDAPI.Models;

namespace AMANDAPI.Models
{
    public class ImageController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Image image)
        {
            if (image == null)
            {
                return BadRequest();
            }

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