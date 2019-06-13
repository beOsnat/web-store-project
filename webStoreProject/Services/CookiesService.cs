using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using webStoreProject.Models;

namespace webStoreProject.Services
{//המטרה של הסרביס - לאפשר את הצגת תוכן העגלה. משמע המרה מרשימת של איי.די ש
    // ששמור בפורמט ג'ייסון בקוקיז לרשימה של מוצרים
    public class CookiesService : ICookiesService
    {
        private IProductRepository _productRepository;

        
        public CookiesService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        public List<Product> ShowCart(HashSet<int> cartProductsId)
        {
            var products = from product in _productRepository.Products() //ברפוזיטורי  מחזירים רשימת אוביקטים מסוג מוצר
                           where cartProductsId.Contains(product.ProductKey) // מחפשים את המוצרים שנמצאים בפרמטר שקיבלתי למתודה -משמע מה שנמצא לי בעגלה
                           select product;
            return products.ToList();
        }
    }
}
