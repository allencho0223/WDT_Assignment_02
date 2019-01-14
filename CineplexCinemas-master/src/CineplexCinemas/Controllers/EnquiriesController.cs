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
    public class EnquiriesController : Controller
    {
        private readonly CineplexDatabaseContext _context;

        public EnquiriesController(CineplexDatabaseContext context)
        {
            _context = context;    
        }

        // GET: Enquiries
        public async Task<IActionResult> Index()
        {
            return View(await _context.Enquiry.ToListAsync());
        }

        // GET: Events
        public IActionResult Events()
        {
            return View();
        }

        // GET: Success Message
        public IActionResult Success()
        {
            return View();
        }

        // GET: Enquiries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enquiry = await _context.Enquiry.SingleOrDefaultAsync(m => m.EnquiryId == id);
            if (enquiry == null)
            {
                return NotFound();
            }

            return View(enquiry);
        }
        
        // POST: Enquiries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EnquiryId,Email,Message,EventDate")] Enquiry enquiry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enquiry);
                await _context.SaveChangesAsync();
                return RedirectToAction("Success");
            }
            return View(enquiry);
        }

        // GET: Enquiries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enquiry = await _context.Enquiry.SingleOrDefaultAsync(m => m.EnquiryId == id);
            if (enquiry == null)
            {
                return NotFound();
            }
            return View(enquiry);
        }

        // POST: Enquiries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EnquiryId,Email,Message,EventDate")] Enquiry enquiry)
        {
            if (id != enquiry.EnquiryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enquiry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnquiryExists(enquiry.EnquiryId))
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
            return View(enquiry);
        }

        // GET: Enquiries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enquiry = await _context.Enquiry.SingleOrDefaultAsync(m => m.EnquiryId == id);
            if (enquiry == null)
            {
                return NotFound();
            }

            return View(enquiry);
        }

        // POST: Enquiries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enquiry = await _context.Enquiry.SingleOrDefaultAsync(m => m.EnquiryId == id);
            _context.Enquiry.Remove(enquiry);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool EnquiryExists(int id)
        {
            return _context.Enquiry.Any(e => e.EnquiryId == id);
        }
    }
}
