using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace _08.Users_and_Products
{
    [XmlType("User")]
    public class UsersAndProductsDto
    {
        [XmlElement("firstName")]
        public string FirstName { get; set; }

        [XmlElement("lastName")]
        public string LastName { get; set; }

        [XmlElement("age")]
        public int? Age { get; set; }

        [XmlElement("SoldProducts")]
        public SoldProductDto SoldProductDto { get; set; }
    }
}
