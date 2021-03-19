using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarDBLibrary.Models
{
    public class ModificationCatalog : BaseModel
    {
        [StringLength(20)]
        public string StartProductionYear { get; set; }
        public ModelCatalog ModelCatalog { get; set; }
        public Guid ModelCatalogId { get; set; }
        public List<VersionCatalog> VersionCatalogs { get; set; }
    }
}
