using ProductShop.Data;
using ProductShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _06.Sold_Products
{
    class Program
    {
        static void Main()
        {
            using (var context = new ProductShopContext())
            {
                var result = GetSoldProducts(context);
                Console.WriteLine(result);
            }
        }

        public static string GetSoldProducts(ProductShopContext context)
        {
            var users = context.Users
                .Where(u => u.ProductsSold != null)
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Select(u => new
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Products = u.ProductsSold.Select(p => new
                    {
                        p.Name,
                        p.Price
                    })
                })
                .Take(5)
                .ToList();

            ;

            return "";
        }
    }
}
