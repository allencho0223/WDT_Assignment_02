using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CineplexCinemas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;


namespace CineplexCinemas.Controllers
{
    public class CartController : Controller
    {
        private int items = 0;
        public int itemCounter = 0;
        public IActionResult Index()
        {
            var context = HttpContext.Session.GetSession<cartItem>("cartItem");
            if (context == null)
            {
                items = 0;
                ViewBag.EmptyCart = "There are currently no items in the cart!";
                return View();
            }

            foreach (var item in context)
            {
                if (items == 0)
                {
                    items++;
                    ViewBag.CartItemNo = item.cartId;
                    ViewBag.LocationId = item.cineplxId;
                    ViewBag.MovieID = item.movieId;
                    ViewBag.SessionID = item.sessionId;
                    ViewBag.Name = item.customerName;
                    ViewBag.noAdults = item.numberOfAdults;
                    ViewBag.noConcession = item.numberOfConc;
                    ViewBag.WarningTime = "Please note that your session will expire in a 10 minute timeframe. If you do not complete your booking within this period, or if you are inactive.";
                }
                else if (items == 1)
                {
                    items++;
                    ViewBag.CartItemNo = item.cartId;
                    ViewBag.LocationId1 = item.cineplxId;
                    ViewBag.MovieID1 = item.movieId;
                    ViewBag.SessionID1 = item.sessionId;
                    ViewBag.Name1 = item.customerName;
                    ViewBag.noAdults1 = item.numberOfAdults;
                    ViewBag.noConcession1 = item.numberOfConc;
                }
                else if(items == 2)
                {
                    items++;
                    ViewBag.CartItemNo = item.cartId;
                    ViewBag.LocationId2 = item.cineplxId;
                    ViewBag.MovieID2 = item.movieId;
                    ViewBag.SessionID2 = item.sessionId;
                    ViewBag.Name2 = item.customerName;
                    ViewBag.noAdults2 = item.numberOfAdults;
                    ViewBag.noConcession2 = item.numberOfConc;
                }
                else if (items == 3)
                {
                    items++;
                    ViewBag.CartItemNo = item.cartId;
                    ViewBag.LocationId3 = item.cineplxId;
                    ViewBag.MovieID3 = item.movieId;
                    ViewBag.SessionID3 = item.sessionId;
                    ViewBag.Name3 = item.customerName;
                    ViewBag.noAdults3 = item.numberOfAdults;
                    ViewBag.noConcession3 = item.numberOfConc;
                }
                else if (items == 4)
                {
                    items++;
                    ViewBag.CartItemNo = item.cartId;
                    ViewBag.LocationId4 = item.cineplxId;
                    ViewBag.MovieID4 = item.movieId;
                    ViewBag.SessionID4 = item.sessionId;
                    ViewBag.Name4 = item.customerName;
                    ViewBag.noAdults4 = item.numberOfAdults;
                    ViewBag.noConcession4 = item.numberOfConc;
                }
                else
                {
                    ViewBag.LocationId = null;
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult confirmationToCart(cartItem booking)
        {
            var cartList = HttpContext.Session.GetSession<cartItem>("cartItem");
            if (cartList == null)
            {
                cartList = new List<cartItem>();
            }
            var numberOfList = HttpContext.Session.GetInt32("itemsInCart");
            if (numberOfList == null)
            {
                numberOfList = default(int);
                numberOfList = 1;
                HttpContext.Session.SetInt32("itemsInCart", numberOfList.GetValueOrDefault());
            }
            else
            {
                HttpContext.Session.SetInt32("itemsInCart", numberOfList.GetValueOrDefault() + 1);
            }

            if (numberOfList <= 5)
            {
                cartItem item = new cartItem();
                item.cartId = itemCounter;
                item.cineplxId = booking.cineplxId;
                item.movieId = booking.movieId;
                item.sessionId = booking.sessionId;
                item.customerName = booking.customerName;
                item.numberOfAdults = booking.numberOfAdults;
                item.numberOfConc = booking.numberOfConc;
                cartList.Add(item);
                HttpContext.Session.SetSession("cartItem", cartList);

                return RedirectToAction("Index", "Cart");
            }
            else
            {
                return RedirectToAction("errorCartFull", "Cart");
            }
        }

        public IActionResult DeleteFromCart(int itemId)
        {
            var context = HttpContext.Session.GetSession<cartItem>("cartItem");
            foreach (var booking in context)
            {
                if(booking.cartId.Equals(itemId))
                {
                    context.Remove(booking);
                    --itemCounter;
                    HttpContext.Session.SetSession("cartItem", context);
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult errorCartFull()
        {
            return View();
        }
    }
}
