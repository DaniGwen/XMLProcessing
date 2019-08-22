using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace _08.Users_and_Products
{
    [XmlType("Users")]
    public class ExportCustomUserDto
    {
        [XmlElement("count")]
        public int Count { get; set; }

        [XmlArray("users")]
        public UsersAndProductsDto[] UsersAndProductsDto { get; set; }
    }
}
