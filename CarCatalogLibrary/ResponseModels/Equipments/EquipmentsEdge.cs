using System;
using System.Collections.Generic;
using System.Text;

namespace CarCatalogLibrary.ResponseModels.Equipments
{
    public class EquipmentsEdge
    {
        public EquipmentsNode node { get; set; }
        public string cursor { get; set; }
    }
}
