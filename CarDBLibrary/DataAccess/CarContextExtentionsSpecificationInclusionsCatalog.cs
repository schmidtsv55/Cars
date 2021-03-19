using CarDBLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDBLibrary.DataAccess
{
    public static class CarContextExtentionsSpecificationInclusionsCatalog
    {
        public static async Task<SpecificationInclusionCatalog> FindOrCreateSpecificationInclusionsCatalogAsync(
            this CarContext carContext,
            Guid inclusionCatalogId,
            Guid specificationCatalogId
            )
        {
            var query = @"EXECUTE [dbo].[FindOrCreateSpecificationInclusionCatalog] 
                          @InclusionCatalogId,
                          @SpecificationCatalogId";

            var parIncId = CarContextExtentions.CreateParGuid("@InclusionCatalogId", inclusionCatalogId);
            var parSpecId = CarContextExtentions.CreateParGuid("@SpecificationCatalogId", specificationCatalogId);
            return carContext.SpecificationInclusionCatalog.FromSqlRaw(query, parIncId, parSpecId).AsEnumerable().First();
        }
    }
}
