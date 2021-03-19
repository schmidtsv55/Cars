using System;
using System.Collections.Generic;
using System.Text;

namespace CarDBLibrary.Models
{
    public class EquipmentVersionCatalog : BaseModel
    {
        public Guid EquipmentCatalogId { get; set; }
        public EquipmentCatalog EquipmentCatalog { get; set; }
        public Guid VersionCatalogId { get; set; }
        public VersionCatalog VersionCatalog { get; set; }
    }
}
