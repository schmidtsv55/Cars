using CarCatalogLibrary.ResponseModels.Equipments;
using CarDBLibrary.DataAccess;
using CarDBLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarLoaderLibrary.Loaders.CatalogLoaders
{
    public class DescriptionsCatalogLoader : BaseCatalogLoader
    {
        public DescriptionsCatalogLoader(
            CarContext carContext,
            CarCatalogLibrary.Client ilsaClient
          ) : base(carContext, ilsaClient) { }

        public async Task<DescriptionCatalog> FindOrCreateDescriptionCatalogAsync(string name)
        {
            return await this.CarContext.FindOrCreateDescriptionCatalogAsync(name);
        }
    }
}
