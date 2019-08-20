using ProductShop.Data;
using ProductShop.Models;
using System;
using System.Collections.Generic;
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

            var categories = xmlDoc.Root.Elements();

            var categoriesList = new List<Category>();

            foreach (var categorie in categories)
            {
                var newCategorie = new Category()
                {
                    Name = categorie.Element("name").Value
                };

                categoriesList.Add(newCategorie);
            }

            context.Categories.AddRange(categoriesList);
            context.SaveChanges();

            return $"Successfully imported {categoriesList.Count}";
        }
    }
}
