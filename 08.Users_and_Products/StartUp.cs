using ProductShop.Data;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace _08.Users_and_Products
{
    class Program
    {
        static void Main()
        {
            using (var context = new ProductShopContext())
            {
                var result = GetUsersWithProducts(context);
                Console.WriteLine(result);
            }
        }

        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var users = context.Users
                .Where(u => u.ProductsSold.Any())
                .Take(5)
                .Select(u => new ExportCustomUserDto
                {
                    Count = u.ProductsSold.Count(),
                    UsersAndProductsDto = context.Users.Select(x => new UsersAndProductsDto
                    {
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Age = u.Age,
                        SoldProductDto = new SoldProductDto
                        {
                            Count = u.ProductsSold.Count(),
                            ProductDto = u.ProductsSold
                        .Select(y => new ProductDto
                        {
                            Name = y.Name,
                            Price = y.Price
                        })
                        .OrderByDescending(y => y.Price)
                        .ToArray()
                        }
                    })
                    .ToArray()
                })
                .OrderByDescending(p => p.Count)
                .ToArray();

            var xmlSerializer = new XmlSerializer(typeof(ExportCustomUserDto[]), new XmlRootAttribute("Users"));

            var sb = new StringBuilder();

            var namespaces = new XmlSerializerNamespaces(new[]
            {
                 XmlQualifiedName.Empty
            });

            xmlSerializer.Serialize(new StringWriter(sb), users, namespaces);

            return sb.ToString();
        }
    }
}
