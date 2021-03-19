using CarCatalogLibrary.ResponseModels.Makes;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace CarCatalogLibrary.ResponseModels.Models
{
    public class ModelsNode
    {
        public string name { get; set; }
        public ResponseModels.Makes.MakesNode make { get; set; } 
    }
}
