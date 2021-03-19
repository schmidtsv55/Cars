using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CarCatalogLibrary.ResponseModels.Pricelist
{
    public class Pricelist
    {
        public string price { get; set; }
        public DateTime? productionFromDate { get; set; }
        public DateTime? validFromDate { get; set; }
    }
}
