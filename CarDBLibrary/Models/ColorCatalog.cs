using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarDBLibrary.Models
{
    public class ColorCatalog : BaseModel
    {
        [StringLength(250)]
        public string Name { get; set; }
        public bool? Metallic { get; set; }
        [StringLength(250)]
        public string Hue { get; set; }
        [StringLength(250)]
        public string Picture { get; set; }
    }
}
