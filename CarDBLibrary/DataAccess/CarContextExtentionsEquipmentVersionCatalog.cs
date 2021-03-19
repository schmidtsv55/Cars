using CarDBLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDBLibrary.DataAccess
{
    public static class CarContextExtentionsEquipmentVersionCatalog
    {
        public static async Task<EquipmentVersionCatalog> FindOrCreateEquipmentVersionCatalogAsync(this CarContext carContext, Guid equipmentCatalogId, Guid versionCatalogId)
        {
            var query = @"EXECUTE [dbo].[FindOrCreateEquipmentVersionCatalog] 
                          @VersionCatalogId,
                          @EquipmentCatalogId";

            var parEqId = CarContextExtentions.CreateParGuid("@EquipmentCatalogId", equipmentCatalogId);
            var parVerId = CarContextExtentions.CreateParGuid("@VersionCatalogId", versionCatalogId);
            return carContext.EquipmentVersionCatalog.FromSqlRaw(query, parEqId, parVerId).AsEnumerable().First();
        }
    }
}
