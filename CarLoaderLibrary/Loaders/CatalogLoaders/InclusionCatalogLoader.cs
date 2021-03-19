using CarCatalogLibrary.ResponseModels.In;
using CarDBLibrary.DataAccess;
using CarDBLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarLoaderLibrary.Loaders.CatalogLoaders
{
    public class InclusionCatalogLoader : BaseCatalogLoader
    {
        public InclusionCatalogLoader(CarContext carContext,
                                 CarCatalogLibrary.Client ilsaClient
           ) : base(carContext, ilsaClient) { }

        public async Task<InclusionCatalog> FindOrCreateEquipmentCatalogAsync(
           bool? standart,
           string description)
        {

            return await this.CarContext.FindOrCreateInclusionCatalogAsync(
                standart,
                description
                );
        }
        public async Task<InclusionCatalog> FindOrCreateEquipmentCatalogAsync(
          InclusionNode inclusionNode)
        {

            return await this.CarContext.FindOrCreateInclusionCatalogAsync(
                inclusionNode.standart,
                inclusionNode.description
                );
        }
    }
}
