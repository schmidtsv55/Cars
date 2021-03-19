using System;
using System.Collections.Generic;
using System.Text;

namespace CarCatalogLibrary.ResponseModels.Models
{
    public class ModelsResponse : BaseInfo
    {
        public List<ModelsEdge> edges { get; set; }
    }
}
