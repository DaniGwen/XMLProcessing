using ProductShop.Data;
using ProductShop.Models;
using System;
using System.Linq;
using System.Xml.Linq;

namespace _04.Import_Categories_and_Products
{
    class Program
    {
        static void Main()
        {
            string inputXml = @"C:\Users\thinkpad\source\repos\XMLProcessing\ProductShopDatabase\Datasets\categories-products.xml";

            using (var context = new ProductShopContext())
            {
                var result = ImportCategoryProducts(context, inputXml);

                Console.WriteLine(result);
            }
        }
        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            XDocument xmlDoc = XDocument.Load(inputXml);
            int imported = 0;
            var categoriesProducts = xmlDoc.Root.Elements();

            foreach (var categorieProduct in categoriesProducts)
            {
                if (categorieProduct.Elements().Count() < 2)
                {
                    continue;
                }

                var contextCategorieProduct = new CategoryProduct()
                {
                    CategoryId = int.Parse(categorieProduct.Element("CategoryId").Value),
                    ProductId = int.Parse(categorieProduct.Element("ProductId").Value)
                };

                context.CategoryProducts.Add(contextCategorieProduct);
                context.SaveChanges();
                imported += 1;
            }

            return $"Successfully imported {imported}";
        }
    }
}
