using ProductShop.Data;
using ProductShop.Models;
using System;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace _01.ImportProducts
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputXml = @"C:\Users\thinkpad\Desktop\ProductShop\ProductShop\Datasets\products.xml";

            using (var context = new ProductShopContext())
            {
                var result = ImportProducts(context, inputXml);
                Console.WriteLine(result);
            }
        }

        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            XDocument xmlDoc = XDocument.Load(inputXml);

            var products = xmlDoc.Root.Elements();

            var productsImported = 0;

            foreach (var product in products)
            {
                var productName = product.Element("name").Value;
                decimal price = decimal.Parse(product.Element("price").Value);
                int sellerId = int.Parse(product.Element("sellerId").Value);

                Product xmlProduct = new Product()
                {
                    Name = productName,
                    Price = price,
                    SellerId = sellerId
                };

                if (product.Elements().Count() > 3)
                {
                    string buyerID = product.Element("buyerId").Value;
                    xmlProduct.BuyerId = int.Parse(buyerID);
                }

                context.Products.Add(xmlProduct);
                productsImported += 1;
            }

            context.SaveChanges();

            return $"Successfully imported {productsImported}";
        }
    }
}
