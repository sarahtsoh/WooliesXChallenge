using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WooliesChallenge.Models;

namespace WooliesChallengeUnitTest
{
    public static class TestData
    {
        public static List<ShopperHistory> GetCustomerHistory()
        {
            var shopperHistories = new List<ShopperHistory>();
            shopperHistories.Add(new ShopperHistory()
            {
                customerId = "123",
                products = GetCustomerShoppingList()
            });

            shopperHistories.Add(new ShopperHistory()
            {
                customerId = "321",
                products = GetCustomerShoppingList()
            });

            return shopperHistories;

        }

        public static List<Product> GetCustomerShoppingList()
        {
            var productList = new List<Product>();
            productList.Add(new Product()
            {
                name = "Test Product A",
                price = 10,
                quantity = 6
            });

            productList.Add(new Product()
            {
                name = "Test Product B",
                price = 5,
                quantity = 8
            });

            return productList.OrderByDescending(c=>c.quantity).ToList();

        }


        public static List<Product> GetProduct()
        {
            var productList = new List<Product>();
            productList.Add(new Product()
            {
                name = "Test Product A",
                price = 10,
                quantity = 0
            });

            productList.Add(new Product()
            {
                name = "Test Product B",
                price = 5,
                quantity = 0
            });
            
            productList.Add(new Product()
            {
                name = "Test Product C",
                price = 3,
                quantity = 0
            });

            return productList;

        }

    }
}
