using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarDBLibrary.Models
{
    public class EquipmentCatalog : BaseModel
    {
        public int DatabaseId { get; set; }
        public List<EquipmentVersionCatalog> EquipmentVersionCatalogs { get; set; }
        public List<SpecificationCatalog> SpecificationCatalogs { get; set; }
        [StringLength(50)]
        public string Cursor { get; set; }
    }
}
