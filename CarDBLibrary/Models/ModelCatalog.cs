using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using System.Text;

namespace CarDBLibrary.Models
{
    public class ModelCatalog : BaseModel
    {
        [StringLength(250)]
        public string Name { get; set; }
        public MakeCatalog MakeCatalog { get; set; }
        public Guid MakeCatalogId { get; set; }
    }
}
