using CarDBLibrary.DataAccess;
using CarDBLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace CarLoaderLibrary.Loaders.CatalogLoaders
{
    public class SpecificationInclusionsCatalogLoader : BaseCatalogLoader
    {
        public SpecificationInclusionsCatalogLoader(CarContext carContext,
                                 CarCatalogLibrary.Client ilsaClient
           ) : base(carContext, ilsaClient) { }

        public async Task<SpecificationInclusionCatalog> FindOrCreateSpecificationInclusionCatalogAsync(
          Guid inclusionCatalogId,
           Guid specificationCatalogId)
        {

            return await this.CarContext.FindOrCreateSpecificationInclusionsCatalogAsync(
                inclusionCatalogId,
                specificationCatalogId
                );
        }
    }
}
