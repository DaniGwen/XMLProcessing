using CarDealer.Models;
using System.Xml.Serialization;

namespace _11.Import_Cars
{
    public class PartDto
    {
        [XmlElement("partId")]
        public int PartId { get; set; }
    }
}