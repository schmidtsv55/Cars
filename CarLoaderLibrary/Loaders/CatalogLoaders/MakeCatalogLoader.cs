using CarCatalogLibrary.ResponseModels.Makes;
using CarDBLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLoaderLibrary.Loaders.CatalogLoaders
{
    public class MakeCatalogLoader : BaseCatalogLoader
    {
        public MakeCatalogLoader(CarContext carContext, 
                                  CarCatalogLibrary.Client ilsaClient
            ) : base(carContext, ilsaClient) { }
        public override async Task LoadAsync()
        {
            Task addMarks = null;
            await foreach (var makes in this.IlsaClient.GetAllMakesAsync())
            {
                if (addMarks != null)
                {
                    await addMarks;
                }
                if (addMarks != null && addMarks.IsFaulted)
                {
                    throw addMarks.Exception;
                }
                addMarks = AddMakesAsync(makes.edges);
                
            }
            if (addMarks != null && !addMarks.IsCompleted)
            {
                await addMarks;
            }
            if (addMarks != null && addMarks.IsFaulted)
            {
                throw addMarks.Exception;
            }
        }
        
        private async Task AddMakeAsync(MakesEdge edge)
        {
            CarContext.FindOrCreateMakeCatalogAsync(edge.node.name);
        }
        private async Task AddMakesAsync(List<MakesEdge> edges)
        {
            foreach (var edge in edges)
            {
                await AddMakeAsync(edge);
            }
        }
        private async Task AddMakesAsync(params string[] names)
        {
            CarContext.CreateMakesAsync(names);
        }
    }
}
