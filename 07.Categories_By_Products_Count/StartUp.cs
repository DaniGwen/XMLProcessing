using ProductShop.Data;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

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
                .Select(c => new ProductsCountDto
                {
                    Name = c.Name,
                    ProductsCount = c.CategoryProducts.Count,
                    AveragePrice = c.CategoryProducts.Average(p => p.Product.Price),
                    TotalRevenue = c.CategoryProducts.Sum(p => p.Product.Price)
                })
                .OrderByDescending(p => p.ProductsCount)
                .ThenBy(p => p.TotalRevenue)
                .ToArray();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProductsCountDto[]),
                new XmlRootAttribute("Categories"));

            var sb = new StringBuilder();

            var xmlNameSpaces = new XmlSerializerNamespaces(new[]
            {
                new XmlQualifiedName("","")
            });

            xmlSerializer.Serialize(new StringWriter(sb), categories, xmlNameSpaces);

            return sb.ToString();
        }
    }
}
