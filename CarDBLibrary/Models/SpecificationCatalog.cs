using System;
using System.Collections.Generic;
using System.Text;

namespace CarDBLibrary.Models
{
    public class SpecificationCatalog  : BaseModel
    {
        public EquipmentCatalog EquipmentCatalog { get; set; }
        public Guid EquipmentCatalogId { get; set; }
        public FeatureCatalog FeatureCatalog { get; set; }
        public Guid FeatureCatalogId { get; set; }
    }
}
