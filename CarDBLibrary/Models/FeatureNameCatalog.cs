using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarDBLibrary.Models
{
    public class FeatureNameCatalog : BaseModel
    {
        [StringLength(250)]
        public string Name { get; set; }
    }
}
