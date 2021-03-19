using CarDBLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDBLibrary.DataAccess
{
    public static class CarContextExtentionsFeatureCatalog
    {
        public static async Task<FeatureCatalog> FindOrCreateFeatureCatalogAsync(
            this CarContext carContext,
            Guid descriptionCatalogId,
            string name,
            bool? standart,
            string value) 
        {
            var query = @"EXECUTE [dbo].[FindOrCreateFeatureCatalog] 
                          @DescriptionCatalogId,
                          @Name,
                          @Standart,
                          @Value";

            var parDescId = CarContextExtentions.CreateParGuid("@DescriptionCatalogId", descriptionCatalogId);
            var parName = CarContextExtentions.ParNameInput(name);
            var parStandart = CarContextExtentions.CreateParBit("@Standart", standart);
            var parValue = CarContextExtentions.CreateParNVarchar("@Value", value, 250);
            
            return carContext.FeatureCatalog.FromSqlRaw(
                query, 
                parDescId,
                parName,
                parStandart,
                parValue
                ).AsEnumerable().First();
        }
    }
}
