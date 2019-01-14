using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CineplexCinemas.Models;

namespace CineplexCinemas.Controllers
{
    public class CineplexMoviesController : Controller
    {
        private readonly CineplexDatabaseContext _context;

        public CineplexMoviesController(CineplexDatabaseContext context)
        {
            _context = context;    
        }

        // GET: CineplexMovies
        public async Task<IActionResult> Index()
        {
            var cineplexDatabaseContext = _context.CineplexMovie.Include(c => c.Cineplex).Include(c => c.Movie).Include(c => c.Session);
            return View(await cineplexDatabaseContext.ToListAsync());
        }

        // GET: CineplexMovies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cineplexMovie = await _context.CineplexMovie.SingleOrDefaultAsync(m => m.CineplexId == id);
            if (cineplexMovie == null)
            {
                return NotFound();
            }

            return View(cineplexMovie);
        }

        // GET: CineplexMovies/Create
        public IActionResult Create()
        {
            ViewData["CineplexId"] = new SelectList(_context.Cineplex, "CineplexId", "Location");
            ViewData["MovieId"] = new SelectList(_context.Movie, "MovieId", "LongDescription");
            return View();
        }

        // POST: CineplexMovies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CineplexId,MovieId")] CineplexMovie cineplexMovie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cineplexMovie);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["CineplexId"] = new SelectList(_context.Cineplex, "CineplexId", "Location", cineplexMovie.CineplexId);
            ViewData["MovieId"] = new SelectList(_context.Movie, "MovieId", "LongDescription", cineplexMovie.MovieId);
            return View(cineplexMovie);
        }

        // GET: CineplexMovies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cineplexMovie = await _context.CineplexMovie.SingleOrDefaultAsync(m => m.CineplexId == id);
            if (cineplexMovie == null)
            {
                return NotFound();
            }
            ViewData["CineplexId"] = new SelectList(_context.Cineplex, "CineplexId", "Location", cineplexMovie.CineplexId);
            ViewData["MovieId"] = new SelectList(_context.Movie, "MovieId", "LongDescription", cineplexMovie.MovieId);
            return View(cineplexMovie);
        }

        // POST: CineplexMovies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CineplexId,MovieId")] CineplexMovie cineplexMovie)
        {
            if (id != cineplexMovie.CineplexId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cineplexMovie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CineplexMovieExists(cineplexMovie.CineplexId))
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
            ViewData["CineplexId"] = new SelectList(_context.Cineplex, "CineplexId", "Location", cineplexMovie.CineplexId);
            ViewData["MovieId"] = new SelectList(_context.Movie, "MovieId", "LongDescription", cineplexMovie.MovieId);
            return View(cineplexMovie);
        }

        // GET: CineplexMovies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cineplexMovie = await _context.CineplexMovie.SingleOrDefaultAsync(m => m.CineplexId == id);
            if (cineplexMovie == null)
            {
                return NotFound();
            }

            return View(cineplexMovie);
        }

        // POST: CineplexMovies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cineplexMovie = await _context.CineplexMovie.SingleOrDefaultAsync(m => m.CineplexId == id);
            _context.CineplexMovie.Remove(cineplexMovie);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        
        private bool CineplexMovieExists(int id)
        {
            return _context.CineplexMovie.Any(e => e.CineplexId == id);
        }

        public IActionResult Sessions(string cinemaName, string movieName)
        {
            var cineplexDatabaseContext = _context.CineplexMovie.Include(c => c.Cineplex).Include(c => c.Movie).Include(c => c.Session);
            var cineplexList = cineplexDatabaseContext.ToList();
            lock (cineplexList)
            {
                foreach (var item in cineplexList.ToArray())
                {
                    if (cinemaName != null)
                    {
                        if (!item.Cineplex.Location.Equals(cinemaName))
                        {
                            cineplexList.Remove(item);
                        }
                    }
                    if(movieName != null)
                    {
                        if (!item.Movie.Title.Equals(movieName))
                        {
                            cineplexList.Remove(item);
                        }
                    }
                }
            }
            cineplexList = cineplexList.OrderBy(x => x.Session.SessionDateTime).ToList();
            return View(cineplexList);
        }
    }
}
