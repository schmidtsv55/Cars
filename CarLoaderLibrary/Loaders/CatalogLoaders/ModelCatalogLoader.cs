using CarCatalogLibrary.ResponseModels.Models;
using CarDBLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarLoaderLibrary.Loaders.CatalogLoaders
{
    public class ModelCatalogLoader : BaseCatalogLoader
    {
        public ModelCatalogLoader(CarContext carContext,
                                  CarCatalogLibrary.Client ilsaClient
            ) : base(carContext, ilsaClient) { }
        public override async Task LoadAsync()
        {
            Task addModels = null;
            await foreach (var models in this.IlsaClient.GetAllModelsAsync())
            {
                if (addModels != null)
                {
                    await addModels;
                }
                if (addModels != null && addModels.IsFaulted)
                {
                    throw addModels.Exception;
                }
                addModels = AddModelsAsync(models.edges);

            }
            if (addModels != null && !addModels.IsCompleted)
            {
                await addModels;
            }
            if (addModels != null && addModels.IsFaulted)
            {
                throw addModels.Exception;
            }
        }

        private async Task AddModelAsync(ModelsEdge edge)
        {
            await CarContext.FindOrCreateModelCatalogAsync(edge.node.make.name, edge.node.name);
        }
        private async Task AddModelsAsync(List<ModelsEdge> edges)
        {
            foreach (var edge in edges)
            {
                await AddModelAsync(edge);
            }
        }
       
    }
}
