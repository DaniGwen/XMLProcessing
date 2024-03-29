﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace _09.Import_Suppliers
{
    [XmlType("Supplier")]
    public class ImportSuppliersDto
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("isImporter")]
        public bool IsImporter { get; set; }
    }
}
