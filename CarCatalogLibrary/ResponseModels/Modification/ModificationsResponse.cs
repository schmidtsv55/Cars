using System;
using System.Collections.Generic;
using System.Text;

namespace CarCatalogLibrary.ResponseModels.Modification
{
    public class ModificationsResponse : BaseInfo
    {
        public List<ModificationsEdge> edges { get; set; }
    }
}
