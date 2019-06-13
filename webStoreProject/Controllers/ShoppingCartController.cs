using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using webStoreProject.Models;
using webStoreProject.Services;

namespace webStoreProject.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ICookiesService _cookiesService;
        private ICartService _cartService;

        public ShoppingCartController(ICookiesService cookiesService)
        {
            _cookiesService = cookiesService;
            _cartService = new CartService();
            ViewBag.pageName = "Shopping cart";
        }

        public IActionResult ShowCart()
        {
            HashSet<int> cartProductsId;
            string productsCookiesJson = Request.Cookies["UserProducts"];
            if (string.IsNullOrWhiteSpace(productsCookiesJson))
            {
                cartProductsId = new HashSet<int>();
            }
            else
            {
                cartProductsId = JsonConvert.DeserializeObject<HashSet<int>>(productsCookiesJson);
            }
            List<Product> currentCart = _cookiesService.ShowCart(cartProductsId);

            return View(currentCart);// לא מימשנו את הview
        }

        public IActionResult RemoveFromCart(int id)
        {
            HashSet<int> cartProductsId;
            string productsCookiesJson = Request.Cookies["UserProducts"];
            cartProductsId = JsonConvert.DeserializeObject<HashSet<int>>(productsCookiesJson);
            cartProductsId.Remove(id);

            Response.Cookies.Append("UserProducts",
            JsonConvert.SerializeObject(cartProductsId),
            new CookieOptions { MaxAge = TimeSpan.FromHours(1) });
            return RedirectToAction("ShowCart");

        }

        [HttpPost]
        public IActionResult CompletePurchase(List<double> prices)
        {
            double visitorPrice = _cartService.VisitorCartSum(prices);
            double memberPrice = _cartService.MemberCartSum(prices);

            TempData["visitorPrice"] = visitorPrice;
            TempData["memberPrice"] = memberPrice;

            return RedirectToAction("ShowCart");
        }





    }
}
