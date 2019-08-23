using CarDealer.Data;
using CarDealer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace _10.Import_Parts
{
    class Program
    {
        static void Main()
        {
            string inputXml = File.ReadAllText(@"C:\Users\thinkpad\Documents\GitHub\XMLProcessing\CarDealerDatabase\Datasets\parts.xml");

            using (var context = new CarDealerContext())
            {
                var result = ImportParts(context, inputXml);
            }
        }

        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportPartsDto[]),
                new XmlRootAttribute("Parts"));

            var partsDto = (ImportPartsDto[])xmlSerializer.Deserialize(new StringReader(inputXml));

            var listOfDtos = new List<Part>();

            var suppliersCount = context.Suppliers.Count();

            foreach (var partDto in partsDto)
            {
                if (partDto.SupplierId > suppliersCount)
                {
                    continue;
                }

                var part = new Part
                {
                    Name = partDto.Name,
                    Price = partDto.Price,
                    Quantity = partDto.Quantity,
                    SupplierId = partDto.SupplierId
                };

                listOfDtos.Add(part);
            }

            context.Parts.AddRange(listOfDtos);
            context.SaveChanges();

            return $"Successfully imported {listOfDtos.Count}";
        }
    }
}
