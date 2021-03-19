using CarDBLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDBLibrary.DataAccess
{
    public static class CarContextExtentionsModificationCatalog
    {
        public static async Task<ModificationCatalog> FindOrCreateModificationCatalogAsync
            (
            this CarContext carContext,
            string makeName,
            string modelName,
            string StartProductionYear)
        {
            var query = @"EXECUTE [dbo].[FindOrCreateModificationCatalog] 
                           @MakeName
                          ,@ModelName
                          ,@StartProductionYearT";
            var parMakeName = CarContextExtentions.CreateParNVarchar("@MakeName", makeName, 255);
            var parModelName = CarContextExtentions.CreateParNVarchar("@ModelName", modelName, 255);
            var parStartProductionYear = CarContextExtentions.CreateParNVarchar("@StartProductionYear", StartProductionYear, 10);
           

            return carContext.ModificationCatalog.FromSqlRaw(query, parMakeName, parModelName, parStartProductionYear).AsEnumerable().First();
          
        }
    }
}
