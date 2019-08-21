using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace _07.Categories_By_Products_Count
{
    [XmlType("Category")]
    public class ProductsCountDto
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("count")]
        public int ProductsCount { get; set; }

        [XmlElement("averagePrice")]
        public decimal AveragePrice { get; set; }

        [XmlElement("totalRevenue")]
        public decimal TotalRevenue { get; set; }
    }
}
