using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarDBLibrary.Models
{
    public class MakeCatalog : BaseModel
    {
        [StringLength(250)]
        public string Name { get; set; }
        public List<ModelCatalog> ModelCatalogs { get; set; }
    }
}
