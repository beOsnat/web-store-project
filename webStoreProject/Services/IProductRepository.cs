﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webStoreProject.Models;

namespace webStoreProject.Services
{
   public interface IProductRepository
    {
        List<Product> Products();
        Product FindProduct(int productId);
        bool AddProduct(Product newProduct);
        bool RemoveProduct(int productId);
        bool UpdateProductBuyer(Product updatedProduct);
        bool UpdateProductState(int id);
        List<Product> OrderByTitle();
        List<Product> OrderByDate();
        List<Product> AvailableItems();




    }
}
