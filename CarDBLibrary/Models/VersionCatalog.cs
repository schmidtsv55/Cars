using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarDBLibrary.Models
{
    public class VersionCatalog : BaseModel
    {
        [StringLength(250)]
        public string Name { get; set; }
        public ModificationCatalog ModificationCatalog { get; set; }
        public Guid ModificationCatalogId { get; set; }
        [StringLength(250)]
        public string Picture { get; set; }
    }
}
