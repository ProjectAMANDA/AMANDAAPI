﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AMANDAPI.Models
{
    public class ImageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}