using CarCatalogLibrary.ResponseModels.Modification;
using CarCatalogLibrary.ResponseModels.Specification;
using CarCatalogLibrary.ResponseModels.Versions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarCatalogLibrary.ResponseModels.Equipments
{
    public class EquipmentsNode
    {
        public int databaseId { get; set; }
        public VersionNode version { get; set; }
        public string picture { get; set; }
        public ModificationNode modification { get; set; }
        public List<Colors.ColorNode> colors { get; set; }
        public SpecificationNode specification { get; set; }
    }
}
