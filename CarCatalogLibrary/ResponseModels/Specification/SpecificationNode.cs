using CarCatalogLibrary.ResponseModels.Description;
using CarCatalogLibrary.ResponseModels.Feature;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarCatalogLibrary.ResponseModels.Specification
{
    public class SpecificationNode
    {
        public List<DescriptionNode> description { get; set; }
        public List<FeatureNode> technical { get; set; }
    }
}
