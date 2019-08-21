using ProductShop.Data;
using System;
using System.Linq;

namespace _07.Categories_By_Products_Count
{
    class Program
    {
        static void Main()
        {
            using (var context = new ProductShopContext())
            {
                string result = GetCategoriesByProductsCount(context);
                Console.WriteLine(result);
            }
        }

        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context.Categories
                .Select(c => new
                {
                    name = c.Name,
                    countProducts = c.CategoryProducts.Count,
                    average = c.CategoryProducts.Average(p => p.Product.Price),
                    totalPrice = c.CategoryProducts.Sum(p => p.Product.Price)
                })
                .OrderBy(p => p.countProducts)
                .ThenBy(p => p.totalPrice)
                .ToList();
            ;
            return "";
        }
    }
}
