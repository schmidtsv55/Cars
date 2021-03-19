using CarCatalogLibrary.ResponseModels.Modification;
using CarDBLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarLoaderLibrary.Loaders.CatalogLoaders
{
    class ModificationsCatalogLoader : BaseCatalogLoader
    {
        public ModificationsCatalogLoader(CarContext carContext,
                                  CarCatalogLibrary.Client ilsaClient
            ) : base(carContext, ilsaClient) { }
        public override async Task LoadAsync()
        {
            Task addModifications = null;
            await foreach (var modifications in this.IlsaClient.GetAllModificationsAsync())
            {
                if (addModifications != null)
                {
                    await addModifications;
                }
                if (addModifications != null && addModifications.IsFaulted)
                {
                    throw addModifications.Exception;
                }
                addModifications = AddModificationsAsync(modifications.edges);

            }
            if (addModifications != null && !addModifications.IsCompleted)
            {
                await addModifications;
            }
            if (addModifications != null && addModifications.IsFaulted)
            {
                throw addModifications.Exception;
            }
        }

        private async Task AddModificationAsync(ModificationsEdge edge)
        {
            await CarContext.FindOrCreateModificationCatalogAsync(
                edge.node.model.make.name, 
                edge.node.model.name, 
                edge.node.startProductionYear);
        }
        private async Task AddModificationsAsync(List<ModificationsEdge> edges)
        {
            foreach (var edge in edges)
            {
                await AddModificationAsync(edge);
            }
        }

    }
}
