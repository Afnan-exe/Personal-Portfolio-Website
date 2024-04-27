using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Portfolio.Models;

namespace Portfolio.Controllers
{
    public class ContactsController : Controller
    {
        private readonly _context _context;

        public ContactsController(_context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            if (HttpContext.Session.GetString("i") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            // Consider caching here if data is relatively static
            var contacts = await _context.Contacts.AsNoTracking().ToListAsync();
            return View(contacts);
        }

        public async Task<IActionResult> Details(int id)
        {
            if (HttpContext.Session.GetString("i") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var contact = await _context.Contacts
                .AsNoTracking()
                .SingleOrDefaultAsync(c => c.id == id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("i") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (HttpContext.Session.GetString("i") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var contact = await _context.Contacts
                .AsNoTracking()
                .SingleOrDefaultAsync(c => c.id == id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            if (HttpContext.Session.GetString("i") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var contact = await _context.Contacts
                .AsNoTracking()
                .SingleOrDefaultAsync(c => c.id == id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return View(contact);
            }

            // Checks if the entity exists before trying to update it
            var exists = await _context.Contacts.AnyAsync(c => c.id == contact.id);
            if (!exists)
            {
                return NotFound();
            }

            // Only updates the state to Modified if the entity exists
            _context.Update(contact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
