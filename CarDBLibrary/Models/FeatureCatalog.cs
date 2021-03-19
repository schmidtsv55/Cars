using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarDBLibrary.Models
{
    public class FeatureCatalog : BaseModel
    {
        public DescriptionCatalog DescriptionCatalog { get; set; }
        public Guid DescriptionCatalogId { get; set; }
        public FeatureNameCatalog FeatureNameCatalog { get; set; }
        public Guid FeatureNameCatalogId { get; set; }
        public bool? Standart { get; set; }
        [StringLength(250)]
        public string Value { get; set; }
        public List<SpecificationInclusionCatalog> SpecificationInclusionCatalogs { get; set; }
    }
}
