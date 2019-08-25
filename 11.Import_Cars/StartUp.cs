using CarDealer.Data;
using CarDealer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                var result = ImportCarsXDocument(context, inputXml);
            }
        }

        public static string ImportCarsXDocument(CarDealerContext context, string inputXml)
        {
            XDocument xDocument = XDocument.Load(inputXml);
            var cars = xDocument.Root.Elements().ToArray();
            var listCarsDto = new List<Car>();

            foreach (var car in cars)
            {
                var partId = car.Element("parts").Nodes().ToArray();

                foreach (var part in partId)
                {
                    var id = part.ToString();
                }

                var contextCar = new Car
                {
                    Make = car.Element("make").Value,
                    Model = car.Element("model").Value,
                    TravelledDistance = long.Parse(car.Element("TraveledDistance").Value),

                    
                };
            }

            return "";
        }
        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportCarsDto[]),
                new XmlRootAttribute("Cars"));

            var carsDto = (ImportCarsDto[])xmlSerializer.Deserialize(new StringReader(inputXml));

            var listCarsDto = new List<Car>();

            var maxPartId = context.Parts.Count();

            foreach (var carDto in carsDto)
            {
                var listOfParts = new List<PartCar>();

                //foreach (var part in carDto.Parts)
                //{
                //   if (part.PartId > maxPartId)
                //    {
                //        continue;
                //    }

                //    var partCar = new PartCar
                //    {
                //        PartId = part.PartId
                //    };

                //    listOfParts.Add(partCar);
                //}

                var car = new Car
                {
                  Make = carDto.Make,
                  Model = carDto.Model,
                  TravelledDistance = carDto.TraveledDistance,
                 PartCars = listOfParts
                };

                listCarsDto.Add(car);
            }

            context.Cars.AddRange(listCarsDto);
            context.SaveChanges();

            return $"Successfully imported {listCarsDto.Count}";
        }
    }
}
