using CarDBLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDBLibrary.DataAccess
{
    public static class CarContextExtentionsDescriptionCatalog
    {
        public static async Task<DescriptionCatalog> FindOrCreateDescriptionCatalogAsync(this CarContext carContext, string name) 
        {
            var query = @"EXECUTE [dbo].[FindOrCreateDescriptionCatalog] 
                          @Name";

            var parName = CarContextExtentions.ParNameInput(name);
            return carContext.DescriptionCatalog.FromSqlRaw(query, parName).AsEnumerable().First();
        }
    }
}
