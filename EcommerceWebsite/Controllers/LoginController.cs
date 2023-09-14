using EcommerceWebsite.Entities;
using EcommerceWebsite.Models;
using EcommerceWebsite.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NuGet.Protocol.Plugins;

namespace EcommerceWebsite.Controllers
{
    public  class LoginController : Controller
    {
		private readonly IMemoryCache memoryCache;

		public LoginController(IMemoryCache memoryCache)
		{
			this.memoryCache = memoryCache;
		}
		[HttpPost]
        public IActionResult SignIn(LoginViewModel loginViewModel)
        {
            if(ModelState.IsValid)
            {
                EcommerceDB ecommerceDB = new EcommerceDB();
                User user =ecommerceDB.Users.FirstOrDefault(u => loginViewModel.Email == u.Email && u.Password == loginViewModel.Password);
                Seller seller =ecommerceDB.Sellers.FirstOrDefault(u => loginViewModel.Email == u.Email && u.password == loginViewModel.Password);
				if (user != null && seller == null)
                {
					memoryCache.Set("UserId", user.Id);
					return RedirectToAction("User", "Products");
				}
				if (user == null && seller != null)
                {
					memoryCache.Set("SellerId", seller.Id);
					/*var products = ecommerceDB.Products.Where(p => p.SellerId == seller.Id).ToList();*/
					return RedirectToAction("Seller", "Products");
                }
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


    }
}
