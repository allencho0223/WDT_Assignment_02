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
    public class BookingsController : Controller
    {
        private readonly CineplexDatabaseContext _context;
        private Cineplex cinema;
        private Movie film;
        private Session session;

        private int cinemaId;
        private int movieId;
        private int sessionId;

        private List<int> bookingId = new List<int>();

        public BookingsController(CineplexDatabaseContext context)
        {
            _context = context;    
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            return View(await _context.Booking.ToListAsync());
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking.SingleOrDefaultAsync(m => m.BookingId == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingId,cineplxId,customerName,movieId,numberOfAdults,numberOfConc,sessionId")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking.SingleOrDefaultAsync(m => m.BookingId == id);
            if (booking == null)
            {
                return NotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingId,cineplxId,customerName,movieId,numberOfAdults,numberOfConc,sessionId")] Booking booking)
        {
            if (id != booking.BookingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.BookingId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking.SingleOrDefaultAsync(m => m.BookingId == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Booking.SingleOrDefaultAsync(m => m.BookingId == id);
            _context.Booking.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool BookingExists(int id)
        {
            return _context.Booking.Any(e => e.BookingId == id);
        }
        
        public IActionResult sessionDetails(int cineplexId, int movieId, int sessionId)
        {
            //Store Id's
            cinemaId = cineplexId;
            this.movieId = movieId;
            this.sessionId = sessionId;

            var cinemaList = _context.Cineplex.ToList();
            var movieList = _context.Movie.ToList();
            var sessionList = _context.Session.ToList();

            cinema = cinemaList.Find(e => e.CineplexId == cineplexId);
            film = movieList.Find(e => e.MovieId == movieId);
            session = sessionList.Find(e => e.SessionId == sessionId);

            if (cinema != null && film != null && session != null)
            {
                ViewData["CinemaId"] = cinemaId;
                ViewData["movieId"] = movieId;
                ViewData["sessionId"] = sessionId;
                ViewData["Location"] = cinema.Location;
                ViewData["Film"] = film.Title;
                ViewData["SessionTime"] = session.SessionDateTime.TimeOfDay.ToString();
                ViewData["SessionDate"] = session.SessionDateTime.Day + "/" + session.SessionDateTime.Month + "/" + session.SessionDateTime.Year;
            }
            return View();
        }
        
        public async Task<IActionResult> finaliseBookings()
        {
            var bookingList = HttpContext.Session.GetSession<int>("bookingSession");
            if(bookingList == null)
            {
                bookingList = new List<int>();
            }
            var cartList = HttpContext.Session.GetSession<cartItem>("cartItem");
            foreach (var booking in cartList)
            {
                Booking newBooking = new Booking();
                newBooking.cineplxId = booking.cineplxId;
                newBooking.movieId = booking.movieId;
                newBooking.sessionId = booking.sessionId;
                newBooking.customerName = booking.customerName;
                newBooking.numberOfAdults = booking.numberOfAdults;
                newBooking.numberOfConc = booking.numberOfConc;

                if (ModelState.IsValid)
                {
                    _context.Add(newBooking);
                    var List =_context.Booking.ToList();
                    var last = List.Last();
                    bookingList.Add(last.BookingId+1);
                    reduceSeatNumbers(newBooking.sessionId, newBooking.numberOfAdults, newBooking.numberOfConc);
                    await _context.SaveChangesAsync();
                }
            }
            cartList.Clear();
            HttpContext.Session.SetSession("cartItem", cartList);
            HttpContext.Session.SetBookingSession("bookingSession", bookingList);
            return RedirectToAction("confirmationPage");
        }

        private void reduceSeatNumbers(int sessionId, int numberOfAdults, int numberOfConc)
        {
            var sessionList = _context.Session.ToList();
            
            foreach(var item in sessionList)
            {
                if(item.SessionId.Equals(sessionId))
                {
                    item.SeatsAvailable = item.SeatsTotal - (numberOfAdults + numberOfConc);
                }
            }

        }
        public IActionResult confirmationPage()
        {
            var bookingList = HttpContext.Session.GetSession<int>("bookingSession");
            ViewBag.bookingIds = bookingList;
            List<Booking> bookingDetails = new List<Booking>();
            List<Session> sessionList = new List<Session>();
            List<Cineplex> cineplexList = new List<Cineplex>();
            foreach(var bookingNumber in bookingList)
            {
                var details = getBookings(bookingNumber);
                if(details != null)
                {
                    bookingDetails.Add(details);
                }
                var sessionDetails = getSession(details.sessionId);
                if(sessionDetails != null)
                {
                    sessionList.Add(sessionDetails);
                }
                var cineplexDetails = getCineplex(details.cineplxId);
                if(cineplexDetails != null)
                {
                    cineplexList.Add(cineplexDetails);
                }
            }
            ViewBag.bookingDetails = bookingDetails;
            ViewBag.sessionList = sessionList;
            ViewBag.cineplexList = cineplexList;
            return View();
        }

        private Booking getBookings(int id)
        {
            var bookingsList = _context.Booking.ToList();
            foreach(var booking in bookingsList)
            {
                if(booking.BookingId.Equals(id))
                {
                    return booking;
                }
            }
            return null;
        }

        private Session getSession(int id)
        {
            var sessionsList = _context.Session.ToList();
            foreach (var session in sessionsList)
            {
                if (session.SessionId.Equals(id))
                {
                    return session;
                }
            }
            return null;
        }

        private Cineplex getCineplex(int id)
        {
            var cineplexList = _context.Cineplex.ToList();
            foreach (var cineplex in cineplexList)
            {
                if (cineplex.CineplexId.Equals(id))
                {
                    return cineplex;
                }
            }
            return null;
        }
    }
}
