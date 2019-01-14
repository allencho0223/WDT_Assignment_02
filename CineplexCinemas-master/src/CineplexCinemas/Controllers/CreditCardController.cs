using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CineplexCinemas.Models;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CineplexCinemas.Controllers
{
    public class CreditCardController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index(CheckOut checkout)
        {
            HttpContext.Session.SetString("nameOfPurchaser", checkout.FullName);
            return View();
        }
    }
}
