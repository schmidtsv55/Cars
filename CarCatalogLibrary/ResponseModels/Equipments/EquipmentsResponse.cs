using System;
using System.Collections.Generic;
using System.Text;

namespace CarCatalogLibrary.ResponseModels.Equipments
{
    public class EquipmentsResponse : BaseInfo
    {
        public List<EquipmentsEdge> edges { get; set; }
    }
}
