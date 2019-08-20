using ProductShop.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace _06.Sold_Products
{
    [XmlType("user")]
    public class SoldProductsDto
    {
        [XmlElement("firstName")]
        public string FirstName { get; set; }

        [XmlElement("lastName")]
        public string LastName { get; set; }

        [XmlElement("products")]
        public List<Product> SoldProducts { get; set; }
    }
}
