using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EcommerceWebsite.Entities;
using EcommerceWebsite.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.Extensions.Caching.Memory;
using Humanizer.Localisation.TimeToClockNotation;

namespace EcommerceWebsite.Controllers
{
    public class ProductsController : Controller
    {
        private EcommerceDB _context = new EcommerceDB();
        private readonly IMemoryCache memoryCache;
        public ProductsController(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }
        // GET: Products
        public async Task<IActionResult> Index()
        {
            var ecommerceDB = _context.Products.Include(p => p.Seller);
            return View(await ecommerceDB.ToListAsync());
        }
		public IActionResult Seller()
        {
			memoryCache.TryGetValue("SellerId", out int? SellerId);
            if (SellerId != null)
            {
                var products = _context.Products.Where(p => p.SellerId == SellerId).ToList();
                ViewBag.SellerId = SellerId;
                ViewBag.products = products;
            }
            return View();
        }public IActionResult User()
        {
            var products = _context.Products.ToList();
            return View(products);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Seller)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            memoryCache.TryGetValue("SellerId", out int? SellerId);
            if (SellerId != null)
            {
                ViewBag.SellerId = SellerId;
            }
            else
            {
                return Content("Error");
            }
            
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.ImageFile != null && product.ImageFile.Length > 0)
                {
                    string fileName = Path.GetFileName(product.ImageFile.FileName);
                    string path = Path.Combine("wwwroot/Assets/Uploads/", Guid.NewGuid().ToString() + fileName);
                    product.Image = Path.GetFileName(path);
                    using (FileStream fs = System.IO.File.Create(path))
                    {
                        product.ImageFile.CopyTo(fs);
                    }
                }
                if (product.ImageFile2 != null && product.ImageFile2.Length > 0)
                {
                    string fileName2 = Path.GetFileName(product.ImageFile2.FileName);
                    string path2 = Path.Combine("wwwroot/Assets/Uploads/", Guid.NewGuid().ToString() + fileName2);
                    product.Image2 = Path.GetFileName(path2); ;
                    using (FileStream fs = System.IO.File.Create(path2))
                    {
                        product.ImageFile2.CopyTo(fs);
                    }

                }
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("Seller");
            }
            ViewData["SellerId"] = new SelectList(_context.Sellers, "Id", "Id", product.SellerId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(Id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["SellerId"] = new SelectList(_context.Sellers, "Id", "Id", product.SellerId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (product.ImageFile != null && product.ImageFile.Length > 0)
                    {
                        string fileName = Path.GetFileName(product.ImageFile.FileName);
                        string path = Path.Combine("wwwroot/Assets/Uploads/", Guid.NewGuid().ToString() + fileName);
                        product.Image = Path.GetFileName(path);
                        using (FileStream fs = System.IO.File.Create(path))
                        {
                            product.ImageFile.CopyTo(fs);
                        }
                    }
                    if (product.ImageFile2 != null && product.ImageFile2.Length > 0)
                    {
                        string fileName2 = Path.GetFileName(product.ImageFile2.FileName);
                        string path2 = Path.Combine("wwwroot/Assets/Uploads/", Guid.NewGuid().ToString() + fileName2);
                        product.Image2 = Path.GetFileName(path2);
                        using (FileStream fs = System.IO.File.Create(path2))
                        {
                            product.ImageFile2.CopyTo(fs);
                        }

                    }
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Seller","Products");
            }
            ViewData["SellerId"] = new SelectList(_context.Sellers, "Id", "Id", product.SellerId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Seller)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'EcommerceDB.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Seller","Products");
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
