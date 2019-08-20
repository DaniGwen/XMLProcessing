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

            var namespaces = new XmlSerializerNamespaces(new[]
            {
                new XmlQualifiedName("","")
            });

            var sb = new StringBuilder();

            var xmlSerializer = new XmlSerializer(typeof(SoldProductsDto[]), new XmlRootAttribute("users"));

            xmlSerializer.Serialize(new StringWriter(sb), users,namespaces);

            return sb.ToString();
        }
    }
}
