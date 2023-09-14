using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EcommerceWebsite.Entities;
using EcommerceWebsite.Models;

namespace EcommerceWebsite.Controllers
{
    public class IbnEl4e5Controller : Controller
    {
        private readonly EcommerceDB _context;

        public IbnEl4e5Controller(EcommerceDB context)
        {
            _context = context;
        }

        // GET: IbnEl4e5
        public async Task<IActionResult> Index()
        {
              return _context.IbnEl4e5s != null ? 
                          View(await _context.IbnEl4e5s.ToListAsync()) :
                          Problem("Entity set 'EcommerceDB.IbnEl4e5s'  is null.");
        }

        // GET: IbnEl4e5/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.IbnEl4e5s == null)
            {
                return NotFound();
            }

            var ibnEl4e5 = await _context.IbnEl4e5s
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ibnEl4e5 == null)
            {
                return NotFound();
            }

            return View(ibnEl4e5);
        }

        // GET: IbnEl4e5/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: IbnEl4e5/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Age")] IbnEl4e5 ibnEl4e5)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ibnEl4e5);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ibnEl4e5);
        }

        // GET: IbnEl4e5/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.IbnEl4e5s == null)
            {
                return NotFound();
            }

            var ibnEl4e5 = await _context.IbnEl4e5s.FindAsync(id);
            if (ibnEl4e5 == null)
            {
                return NotFound();
            }
            return View(ibnEl4e5);
        }

        // POST: IbnEl4e5/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Age")] IbnEl4e5 ibnEl4e5)
        {
            if (id != ibnEl4e5.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ibnEl4e5);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IbnEl4e5Exists(ibnEl4e5.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ibnEl4e5);
        }

        // GET: IbnEl4e5/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.IbnEl4e5s == null)
            {
                return NotFound();
            }

            var ibnEl4e5 = await _context.IbnEl4e5s
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ibnEl4e5 == null)
            {
                return NotFound();
            }

            return View(ibnEl4e5);
        }

        // POST: IbnEl4e5/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.IbnEl4e5s == null)
            {
                return Problem("Entity set 'EcommerceDB.IbnEl4e5s'  is null.");
            }
            var ibnEl4e5 = await _context.IbnEl4e5s.FindAsync(id);
            if (ibnEl4e5 != null)
            {
                _context.IbnEl4e5s.Remove(ibnEl4e5);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IbnEl4e5Exists(int id)
        {
          return (_context.IbnEl4e5s?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
