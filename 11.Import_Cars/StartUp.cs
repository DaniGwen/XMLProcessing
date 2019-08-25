using CarDealer.Data;
using CarDealer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace _11.Import_Cars
{
    class Program
    {
        static void Main()
        {
            string inputXml = @"C:\Users\thinkpad\Documents\GitHub\XMLProcessing\CarDealerDatabase\Datasets\cars.xml";

            using (var context = new CarDealerContext())
            {
                var result = ImportCars(context, inputXml);
            }
        }

        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            XDocument xDocument = XDocument.Load(inputXml);
            var cars = xDocument.Root.Elements().ToArray();
            var listOfCars = new List<Car>();

            int maxPartsId = context.Parts.Count();

            foreach (var car in cars)
            {
                var partId = car.Element("parts").Nodes().ToArray();
                var listOfPartId = new List<PartCar>();

                foreach (var part in partId)
                {
                    string pattern = @"[0-9]+";
                    var regex = Regex.Match(part.ToString(), pattern);
                    int id = int.Parse(regex.ToString());

                    if (id > maxPartsId)
                    {
                        continue;
                    }

                    var partItem = new PartCar
                    {
                        PartId = id
                    };

                    listOfPartId.Add(partItem);
                }

                var contextCar = new Car
                {
                    Make = car.Element("make").Value,
                    Model = car.Element("model").Value,
                    TravelledDistance = long.Parse(car.Element("TraveledDistance").Value),
                    PartCars = listOfPartId
                };

                listOfCars.Add(contextCar);
            }

            context.Cars.AddRange(listOfCars);
            context.SaveChanges();

            return $"Successfully imported {listOfCars.Count}";
        }
    }
}
