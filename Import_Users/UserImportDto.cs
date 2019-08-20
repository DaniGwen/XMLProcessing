using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

[XmlType("User")]
public class UserImportDto
{
    [XmlElement("firstName")]
    public string FirstName { get; set; }

    [XmlElement("lastName")]
    public string  LastName { get; set; }

    [XmlElement("age")]
    public int Age { get; set; }
}

