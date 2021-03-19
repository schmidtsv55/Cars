using CarCatalogLibrary.ResponseModels.Feature;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarCatalogLibrary.ResponseModels.Description
{
    public class DescriptionNode
    {
        public string name { get; set; } = "Дополнительно";
        public List<FeatureNode> features { get; set; }
    }
}
