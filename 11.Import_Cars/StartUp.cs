using CarDealer.Data;
using CarDealer.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace _11.Import_Cars
{
    class Program
    {
        static void Main()
        {
            string inputXml =
                @"C:\Users\thinkpad\Documents\GitHub\XMLProcessing\CarDealerDatabase\Datasets\cars.xml";

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

            var maxPartsId = context.Parts.Count();

            foreach (var car in cars)
            {
                var partElements = car.Element("parts").Nodes().ToArray();

                var myCar = new Car
                {
                    Make = car.Element("make").Value,
                    Model = car.Element("model").Value,
                    TravelledDistance = long.Parse(car.Element("TraveledDistance").Value)
                };

                foreach (var part in partElements)
                {
                    string pattern = @"[0-9]+";
                    var regex = Regex.Match(part.ToString(), pattern);
                    int id = int.Parse(regex.ToString());

                    if (id > maxPartsId)
                    {
                        continue;
                    }

                    myCar.PartCars.Add(new PartCar
                    {
                        PartId = id
                    });
                }

                listOfCars.Add(myCar);
            }

            context.Cars.AddRange(listOfCars);
            context.SaveChanges();

            return $"Successfully imported {listOfCars.Count}";
        }
    }
}