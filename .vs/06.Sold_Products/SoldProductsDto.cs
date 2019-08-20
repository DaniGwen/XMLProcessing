using ProductShop.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace _06.Sold_Products
{
    [XmlType("User")]
    public class SoldProductsDto
    {
        [XmlElement("firstName")]
        public string FirstName { get; set; }

        [XmlElement("lastName")]
        public string LastName { get; set; }

        [XmlArray("soldProducts")]
        public ProductDto[] ProductsDto { get; set; }
    }

    [XmlType("Product")]
    public class ProductDto
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }
    }
}
