using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace _05.Products_In_Range
{
    [XmlType("Product")]
    public class ExportProductDto
    {
        [XmlElement("name")]
        public string Name { get; set; }
        [XmlElement("price")]
        public decimal Price { get; set; }
        [XmlElement("Buyer")]
        public string Buyer { get; set; }
    }
}
