using CarDBLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDBLibrary.DataAccess
{
    public static class CarContextExtentionsEquipmentCatalog
    {
        public static async Task<EquipmentCatalog> FindOrCreateEquipmentCatalogAsync(this CarContext carContext, int databaseId)
        {
            var query = @"EXECUTE [dbo].[FindOrCreateEquipmentCatalog] 
                          @DatabaseId";
            var parDatabaseId = CarContextExtentions.ParDatabaseIdInput(databaseId);
            return carContext.EquipmentCatalog.FromSqlRaw(query, parDatabaseId).AsEnumerable().First();
            
        }
    }
}
