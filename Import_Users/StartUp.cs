
using AutoMapper;
using ProductShop.Data;
using ProductShop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Import_Users
{
    class Program
    {
        static void Main()
        {

            var usersXml = File.ReadAllText(@"..\..\..\Datasets\users.xml");
            
            using (var context = new ProductShopContext())
            {
                var result = ImportUsers(context, usersXml);

                Console.WriteLine(result);
            }
        }
        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(UserImportDto[]),
                new XmlRootAttribute("Users"));

            var usersDto = (UserImportDto[])xmlSerializer.Deserialize(new StringReader(inputXml));

            List<User> usersList = new List<User>();

            foreach (var user in usersDto)
            {
                var newUser = new User
                {
                    Age = user.Age,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };

                usersList.Add(newUser);
            }

            context.Users.AddRange(usersList);
            context.SaveChanges();

            return $"Successfully imported {usersList.Count}";
        }
    }
}
