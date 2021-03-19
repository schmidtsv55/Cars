using System;
using System.Collections.Generic;
using System.Text;

namespace CarDBLibrary.Models
{
    public class SpecificationInclusionCatalog : BaseModel
    {
        public InclusionCatalog InclusionCatalog { get; set; }
        public Guid InclusionCatalogId { get; set; }
        public SpecificationCatalog SpecificationCatalog { get; set; }

        public Guid SpecificationCatalogId { get; set; }
    }
}
