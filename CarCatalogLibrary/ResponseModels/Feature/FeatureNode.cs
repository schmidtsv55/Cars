using CarCatalogLibrary.ResponseModels.In;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarCatalogLibrary.ResponseModels.Feature
{
    public class FeatureNode
    {
        public string name { get; set; }
        public bool? standart { get; set; }
        public string value { get; set; }
        public List<InclusionNode> ins { get; set; }
    }
}
