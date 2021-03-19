using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarDBLibrary.Models
{
    public class InclusionCatalog : BaseModel
    {
        public bool? Standart { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
    }
}
