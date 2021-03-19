using System;
using System.Collections.Generic;
using System.Text;

namespace CarCatalogLibrary.ResponseModels.Colors
{
    public class ColorNode
    {
        public string name { get; set; }
        public bool? metallic { get; set; }
        public string hue { get; set; }
        public Pricelist.Pricelist pricelist { get; set; }
        public string picture { get; set; }
    }
}
