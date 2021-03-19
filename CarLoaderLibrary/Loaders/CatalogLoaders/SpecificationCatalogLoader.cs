using CarDBLibrary.DataAccess;
using CarDBLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarLoaderLibrary.Loaders.CatalogLoaders
{
    public class SpecificationCatalogLoader : BaseCatalogLoader
    {
        public SpecificationCatalogLoader(CarContext carContext,
                                 CarCatalogLibrary.Client ilsaClient
           ) : base(carContext, ilsaClient) { }

        public async Task<SpecificationCatalog> FindOrCreateSpecificationCatalogAsync(
           Guid equipmentCatalogId,
            Guid featureCatalogId)
        {

            return await this.CarContext.FindOrCreateSpecificationCatalogAsync(
                equipmentCatalogId,
                featureCatalogId
                );
        }
        public async Task ActivateNewSpecificationCatalogAsync(
           Guid equipmentCatalogId
            )
        {

            await this.CarContext.ActivateNewSpecificationCatalogAsync(
                equipmentCatalogId
                );
        }
    }
}
