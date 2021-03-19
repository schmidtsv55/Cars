using CarDBLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDBLibrary.DataAccess
{
    public static class CarContextExtentionsSpecificationCatalog
    {
        public static async Task<SpecificationCatalog> FindOrCreateSpecificationCatalogAsync(
            this CarContext carContext, 
            Guid equipmentCatalogId, 
            Guid featureCatalogId
            )
        {
            var query = @"EXECUTE [dbo].[FindOrCreateSpecificationCatalog]                           
                          @EquipmentCatalogId,
                          @FeatureCatalogId";

            var parEqId = CarContextExtentions.CreateParGuid("@EquipmentCatalogId", equipmentCatalogId);
            var parFeatId = CarContextExtentions.CreateParGuid("@FeatureCatalogId", featureCatalogId);
            return carContext.SpecificationCatalog.FromSqlRaw(query, parEqId, parFeatId).AsEnumerable().First();
        }
        public static async Task ActivateNewSpecificationCatalogAsync(
            this CarContext carContext,
            Guid equipmentCatalogId
            ) 
        {
            var query = @"EXECUTE [dbo].[ActivateNewSpecificationCatalog]                           
                          @EquipmentCatalogId";
            var parEqId = CarContextExtentions.CreateParGuid("@EquipmentCatalogId", equipmentCatalogId);
            carContext.Database.ExecuteSqlRaw(query, parEqId);
        }
    }
}
