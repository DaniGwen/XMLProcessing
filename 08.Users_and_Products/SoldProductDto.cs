using System.Xml.Serialization;

namespace _08.Users_and_Products
{
    
    public class SoldProductDto
    {
        [XmlElement("count")]
        public int Count { get; set; }

        [XmlArray("Products")]
        public ProductDto[] ProductDto { get; set; }
    }
}