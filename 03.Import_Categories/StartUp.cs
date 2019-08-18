using ProductShop.Data;
using ProductShop.Models;
using System;
using System.Xml.Linq;

namespace _03.Import_Categories
{
    class Program
    {
        static void Main()
        {
            string inputXml = @"C:\Users\thinkpad\source\repos\XMLProcessing\ProductShopDatabase\Datasets\categories.xml";

            using (var context = new ProductShopContext())
            {
                var result = ImportCategories(context, inputXml);

                Console.WriteLine(result);
            }
        }

        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            XDocument xmlDoc = XDocument.Load(inputXml);
            int imported = 0;
            var categories = xmlDoc.Root.Elements();

            foreach (var categorie in categories)
            {
                var contextCategorie = new Category()
                {
                    Name = categorie.Element("name").Value
                };

                context.Categories.Add(contextCategorie);
                context.SaveChanges();
                imported += 1;
            }

            return $"Successfully imported {imported}";
        }
    }
}
