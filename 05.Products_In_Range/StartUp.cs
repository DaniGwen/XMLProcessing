using ProductShop.Data;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace _05.Products_In_Range
{
    class Program
    {
        static void Main()
        {
            using (var context = new ProductShopContext())
            {
                var result = GetProductsInRange(context);
                Console.WriteLine(result);
            }
        }

        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context.Products
                .Where(p => p.Price >= 500 || p.Price <= 1000)
                .Select(p => new ExportProductDto
                {
                    Name = p.Name,
                    Price = p.Price,
                    Buyer = p.Buyer.FirstName + " " + p.Buyer.LastName
                })
                .OrderBy(p => p.Price)
                .Take(10)
                .ToArray();

            //Initialize XMLserializer with XML root attribute "Products"
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ExportProductDto[]),
                new XmlRootAttribute("products"));

            //REMOVE TOP NAMESPACE IN XML FILE
            var namespaces = new XmlSerializerNamespaces(new[]
            {
                new XmlQualifiedName("","")
            });

            var sb = new StringBuilder();

            //Write all products in string builder
            xmlSerializer.Serialize(new StringWriter(sb), products, namespaces);

            return sb.ToString();
        }
    }
}
