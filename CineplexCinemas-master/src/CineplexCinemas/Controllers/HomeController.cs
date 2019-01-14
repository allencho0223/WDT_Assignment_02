using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CineplexCinemas.Models;

namespace CineplexCinemas.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Cinemas()
        {
            ViewData["Message"] = "Your Cinemas page.";

            return View();
        }

        public IActionResult Events()
        {
            ViewData["Message"] = "Private Hire";

            return View();
        }

        public IActionResult Movies()
        {
            ViewData["Message"] = "Your Movies page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
