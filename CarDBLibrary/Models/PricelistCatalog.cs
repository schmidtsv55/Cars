using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarDBLibrary.Models
{
    public class PricelistCatalog : BaseModel
    {
        [StringLength(20)]
        public string Price { get; set; }
        public DateTime? ProductionFromDate { get; set; }
        public DateTime? ValidFromDate { get; set; }
        public ColorCatalog ColorCatalog { get; set; }
        public Guid ColorCatalogId { get; set; }
        public EquipmentCatalog EquipmentCatalog { get; set; }
        public Guid EquipmentCatalogId { get; set; }
    }
}
