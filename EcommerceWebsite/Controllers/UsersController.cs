using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcommerceWebsite.Entities;
using EcommerceWebsite.Models;
using System.Xml.Schema;
using Microsoft.Extensions.Caching.Memory;
using System.Net.WebSockets;

namespace EcommerceWebsite.Controllers
{
    public class UsersController : Controller
    {
        private  EcommerceDB _context = new EcommerceDB();
        private readonly IMemoryCache memoryCache;

		public UsersController(IMemoryCache memoryCache) { this.memoryCache = memoryCache; }
        // GET: Users
        public async Task<IActionResult> Index()
        {
              return _context.Users != null ? 
                          View(await _context.Users.ToListAsync()) :
                          Problem("Entity set 'EcommerceDB.Users'  is null.");
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                memoryCache.Set("UserId", user.Id);
                return RedirectToAction("User","Products");
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Password,PhoneNumber")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'EcommerceDB.Users'  is null.");
            }
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
          return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public IActionResult Cart()
        {
            return View(Models.Cart.UserCart);
        }
        [HttpPost]
        public IActionResult AddToCart(int Id)
        {
            Product product = _context.Products.FirstOrDefault(p=>p.Id == Id);
            Models.Cart.UserCart.Add(product);
            return RedirectToAction("Cart");
        }
        public IActionResult MakeOrder(decimal Total)
        {
			memoryCache.TryGetValue("UserId", out int UserId);
            User user = _context.Users.FirstOrDefault(u => u.Id == UserId);
			Order order = new Order();
            order.Products = Models.Cart.UserCart;
            order.UserId = UserId;
            order.TotalPrice= Total;
            order.CreatedAt = DateTime.Now;
            string OrderName= string.Empty;
            for (int i = 0; i < Models.Cart.UserCart.Count; i++)
            {
                if(i== Models.Cart.UserCart.Count-1)
                    OrderName += Models.Cart.UserCart[i].Name;
                else
                    OrderName += Models.Cart.UserCart[i].Name+"\n";
            }
            order.Name = OrderName;
            user.Orders.Add(order);
			_context.Update(user);
			_context.SaveChanges();
			return RedirectToAction("Index", "Orders");
        }

	}
}
