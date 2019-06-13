using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webStoreProject.Models;

namespace webStoreProject.Services
{
    public interface ICookiesService
    {
      ///  void AddToCookies(int productId);
        List<Product> ShowCart(HashSet<int> cartProductsId);
    }
}
