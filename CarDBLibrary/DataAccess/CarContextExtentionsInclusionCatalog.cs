using CarDBLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDBLibrary.DataAccess
{
    public static class CarContextExtentionsInclusionCatalog
    {
        public static async Task<InclusionCatalog> FindOrCreateInclusionCatalogAsync(
            this CarContext carContext,
            bool? standart,
            string description)
        {
            var query = @"EXECUTE [dbo].[FindOrCreateInclusionCatalog] 
                          @Standart,
                          @Description";

            var parStandart = CarContextExtentions.CreateParBit("@Standart", standart);
            var parDesc = CarContextExtentions.CreateParNVarchar("@Description", description, 255);

            return carContext.InclusionCatalog.FromSqlRaw(
                query,
                parStandart,
                parDesc
                ).AsEnumerable().First();
        }
    }
}
