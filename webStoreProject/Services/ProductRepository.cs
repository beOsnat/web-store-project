﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webStoreProject.Data;
using webStoreProject.Models;

namespace webStoreProject.Services
{
    public class ProductRepository : IProductRepository
    {
       
        private myStoreDbContext _storeDbContext;

        public ProductRepository(myStoreDbContext storeDbContext)
        {
            _storeDbContext = storeDbContext;
        }

        public Product FindProduct(int productId)
        {
           var foundProduct = Products().Find((product)=> product.ProductKey==productId);
            return foundProduct;
        }

        public bool AddProduct(Product newProduct)
        {
            _storeDbContext.Products.Add(newProduct);
            int addedRows = _storeDbContext.SaveChanges();
            return addedRows > 0;
        }

        public bool RemoveProduct(int productId)
        {
            var product = _storeDbContext.Products.First(p => p.ProductKey == productId);
            _storeDbContext.Products.Remove(product);
            int deleteRows = _storeDbContext.SaveChanges();
            return deleteRows > 0;
        }

        public List<Product> Products()
        {
            return _storeDbContext.Products.ToList();
        }

        // general note 1:since database approaches are expensive we chose to seperate updating product buyer and state to different methods because 
        // wanted to approach only specific datacontext properties that were needed to be updated
       
        public bool UpdateProductBuyer(Product updatedProduct)
        {
            var updated = _storeDbContext.Products.First(p => p.ProductKey == updatedProduct.ProductKey);
            updated.BuyerId = updatedProduct.BuyerId;
            int updatedRows = _storeDbContext.SaveChanges();
            return updatedRows > 0;
        }
    
        public bool UpdateProductState(int id)
        {
            //search the wanted item in the repository and change it's status from available to in cart
            var foundProduct = FindProduct(id);
            foundProduct.ProductState = State.InCart;
            int updated = _storeDbContext.SaveChanges();
            return updated>0;
        }

        //general note 2: since the repository class is used to manage the databasecontext we decided to implement all the linq queries here


        public List<Product> OrderByTitle()
        {
            var orderByTitle = from product in Products()
                               where product.ProductState == State.Available
                               orderby product.Title ascending
                               select product;
            return orderByTitle.ToList();

        }

        public List<Product> OrderByDate()
        {
            var orderByDate = from product in Products()
                              where product.ProductState == State.Available
                              orderby product.PublishedDate
                              select product;
            return orderByDate.ToList();
            
        }

        public List<Product> AvailableItems()
        {
            var availableItems = from product in Products()
                                 where product.ProductState == State.Available
                                 select product;
            return availableItems.ToList();
        }
               


    }
}
