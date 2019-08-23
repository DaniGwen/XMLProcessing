using CarDealer.Data;
using CarDealer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace _09.Import_Suppliers
{
    class Program
    {
        static void Main()
        {
            string inputXml = File.ReadAllText(@"C:\Users\thinkpad\Documents\GitHub\XMLProcessing\CarDealerDatabase\Datasets\suppliers.xml");

            using (var context = new CarDealerContext())
            {
                var result = ImportSuppliers(context, inputXml);
                Console.WriteLine(result);
            }
        }
        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(ImportSuppliersDto[]), new XmlRootAttribute("Suppliers"));

            var suppliersDto = (ImportSuppliersDto[])xmlSerializer.Deserialize(new StringReader(inputXml));

            var listOfSuppliers = new List<Supplier>();

            foreach (var supplier in suppliersDto)
            {
                var newSupplier = new Supplier
                {
                    Name = supplier.Name,
                    IsImporter = supplier.IsImporter
                };

                listOfSuppliers.Add(newSupplier);
            }

            context.Suppliers.AddRange(listOfSuppliers);
            context.SaveChanges();

            return $"Successfully imported {listOfSuppliers.Count}";
        }
    }
}
