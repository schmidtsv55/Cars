using CarDBLibrary.DataAccess;
using CarDBLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarLoaderLibrary.Loaders.CatalogLoaders
{
    public class FeaturesCatalogLoader : BaseCatalogLoader
    {
        public FeaturesCatalogLoader(CarContext carContext,
                                 CarCatalogLibrary.Client ilsaClient
           ) : base(carContext, ilsaClient) { }

        public async Task<FeatureCatalog> FindOrCreateEquipmentCatalogAsync(
            Guid descriptionId,
            string name,
            bool? standart,
            string value)
        {

            return await this.CarContext.FindOrCreateFeatureCatalogAsync(
                descriptionId,
                name,
                standart,
                value
                );
        }
        public async Task<FeatureCatalog> FindOrCreateEquipmentCatalogAsync(
           Guid descriptionId,
           CarCatalogLibrary.ResponseModels.Feature.FeatureNode featureNode)
        {

            return await this.FindOrCreateEquipmentCatalogAsync(
                descriptionId,
                featureNode.name,
                featureNode.standart,
                featureNode.value
                );
        }
    }
}
