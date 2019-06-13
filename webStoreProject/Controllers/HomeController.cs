using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using webStoreProject.Models;
using webStoreProject.Services;

namespace webStoreProject.Controllers
{
    public class HomeController : Controller
    {
        private ICookiesService _cookieService;

        private IProductRepository _productRepository;

        public HomeController(IProductRepository productRepository, ICookiesService cookiesService)
        {
            _productRepository = productRepository;
            _cookieService = cookiesService;
            ViewBag.pageName = "Home";
        }

        public IActionResult Index(string key) //shows the items according to the wanted order by method.
        {
            List<Product> myProductsList = new List<Product>();
            //if (TempData.ContainsKey(key))
            //{
            //    myProductsList
            //        = JsonConvert.DeserializeObject<List<Product>>(TempData[key].ToString());
            //}
            return View(myProductsList);
        }

        public IActionResult AvailableItems() //a method that sends a key to the index Action
        {

            TempData["availableItems"] = JsonConvert.SerializeObject(_productRepository.AvailableItems());
            return RedirectToAction("Index", new { key = "availableItems" });
        }

        public IActionResult OrderByDate()
        {
            TempData["orderByDate"] = JsonConvert.SerializeObject(_productRepository.OrderByDate());
            return RedirectToAction("Index", new { key = "orderByDate" });
        }

        public IActionResult OrderByTitle()
        {
            
            TempData["orderByTitle"] = JsonConvert.SerializeObject(_productRepository.OrderByTitle());
            return RedirectToAction("Index", new { key = "orderByTitle" });

        }

        public IActionResult ShowDetails(int id)
        {
            return View();
        }

        //gets the current item list storaged in cookies and updates it when an item is added
        //goes first to available items action
        public IActionResult AddToCart(int id, State productState)  
        {
            if (productState == State.Available)
            {
                _productRepository.UpdateProductState(id);

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

                cartProductsId.Add(id);
                Response.Cookies.Append("UserProducts",
                    JsonConvert.SerializeObject(cartProductsId),
                    new CookieOptions { MaxAge = TimeSpan.FromHours(1) });
            }
            else
            {
                ViewBag.ErrorMessage = "sorry, the item is not availalbe at the moment";
            }
            return RedirectToAction("AvailableItems", "Home");

        }

        public IActionResult AddNewAdvertisement()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddNewAdvertisement(Product product)
        {
            if (ModelState.IsValid)
            {
                _productRepository.AddProduct(product);
                ViewBag.ItemAdded = "the item was published successfully";
            }
            return View();

        }
        //[HttpPost]
        //public IActionResult Login(string UserName, string enteredPassword) { }
        //public IActionResult Register(User user) {ViewBag.pageName = "Registration"; }


        //public iActionResult About() { ViewBag.pageName = "Details"; }// shows information about the website and it's founders



    }
}

