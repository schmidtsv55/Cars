using System;
using System.Collections.Generic;
using System.Text;

namespace CarCatalogLibrary.ResponseModels.Makes
{
    public class MakesResponse : BaseInfo
    {
        public List<MakesEdge> edges { get; set; }
    }
}
