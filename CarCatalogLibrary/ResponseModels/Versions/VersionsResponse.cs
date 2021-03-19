using System;
using System.Collections.Generic;
using System.Text;

namespace CarCatalogLibrary.ResponseModels.Versions
{
    public class VersionsResponse : BaseInfo
    {
        public List<VersionsEdge> edges { get; set; }
    }
}
