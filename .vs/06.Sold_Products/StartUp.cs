using ProductShop.Data;
using ProductShop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

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
            var products = context.Products
                .ToArray();

            var users = context.Users
                .Where(u => u.ProductsSold.Count > 0)
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Select(u => new SoldProductsDto
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    ProductsDto = u.ProductsSold.Select(p => new ProductDto
                    {
                       Name = p.Name,
                       Price = p.Price
                    }).ToArray()
                })
                .Take(5)
                .ToArray();

            var namespaces = new XmlSerializerNamespaces(new[]
            {
                new XmlQualifiedName("","")
            });

            var sb = new StringBuilder();

            var xmlSerializer = new XmlSerializer(typeof(SoldProductsDto[]), new XmlRootAttribute("Users"));

            xmlSerializer.Serialize(new StringWriter(sb), users,namespaces);

            return sb.ToString();
        }
    }
}
