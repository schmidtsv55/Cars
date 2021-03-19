using CarCatalogLibrary.ResponseModels.Equipments;
using CarDBLibrary.DataAccess;
using CarDBLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarLoaderLibrary.Loaders.CatalogLoaders
{
    public class VersionCatalogLoader : BaseCatalogLoader
    {
        public VersionCatalogLoader(CarContext carContext,
                                  CarCatalogLibrary.Client ilsaClient
            ) : base(carContext, ilsaClient) { }
        public override Task LoadAsync()
        {
            return base.LoadAsync();
        }

        public async Task<VersionCatalog> FindOrCreateVersionCatalogAsync(EquipmentsEdge edge)
        {
            var makeName = edge.node.modification.model.make.name;
            var modelName = edge.node.modification.model.name;
            var versionName = edge.node.version.name;
            var startProductionYear = edge.node.modification.startProductionYear;
            var picture = edge.node.picture;
            return await FindOrCreateVersionCatalogAsync(
                makeName,
                modelName,
                versionName,
                startProductionYear,
                picture);
        }
        public async Task<VersionCatalog> FindOrCreateVersionCatalogAsync(
            string makeName,
            string modelName,
            string versionName,
            string startProductionYear,
            string picture)
        {
            return await CarContext.FindOrCreateVersionCatalog(
                makeName,
                modelName,
                versionName,
                startProductionYear,
                picture);
        }

        public async Task AddVersionsCatalogAsync(List<EquipmentsEdge> edges) 
        {
            foreach (var edge in edges)
            {
                await this.FindOrCreateVersionCatalogAsync(edge);
            }
        }
    }
}
