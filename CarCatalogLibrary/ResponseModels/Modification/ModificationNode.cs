using CarCatalogLibrary.ResponseModels.Colors;
using CarCatalogLibrary.ResponseModels.Makes;
using CarCatalogLibrary.ResponseModels.Models;
using CarCatalogLibrary.ResponseModels.Versions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace CarCatalogLibrary.ResponseModels.Modification
{
    public class ModificationNode
    {
        public ModelsNode model { get; set; }
        public DateTime? releaseDate { get; set; }
        public string startProductionYear { get; set; }
    }
}
