using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CarDBLibrary.DataAccess
{
    public static class CarContextExtentionsMarkCatalog
    {

        public static void CreateMakesAsync(this CarContext carContext, params string[] names) 
        {
            var query = @"EXECUTE [dbo].[CreateMakes] 
                          @Makes";
            carContext.Database.ExecuteSqlRaw(
                query,
                ParTableMakes(names));

        }
        public static Models.MakeCatalog FindOrCreateMakeCatalogAsync(this CarContext carContext, string name)
        {
            var query = @"EXECUTE [dbo].[FindOrCreateMakeCatalog] 
                          @Name
                          ,@Id OUTPUT";
            var parName = CarContextExtentions.ParNameInput(name);
            var parId = CarContextExtentions.ParIdOutput();
            carContext.Database.ExecuteSqlRaw(
                query,
                parName,
                parId);

            var mark = new Models.MakeCatalog
            {
                Id = (Guid)parId.Value,
                Name = name
            };
            return mark;
        }
        private static Microsoft.Data.SqlClient.SqlParameter ParTableMakes(params string[] names)
        {
            return new Microsoft.Data.SqlClient.SqlParameter
            {
                ParameterName = "@Makes",  
                SqlDbType = SqlDbType.Structured,
                Value = CreateMakesTable(names),
                TypeName = "[dbo].[MakeType]"
            };
        }

        private static DataTable CreateMakesTable(params string[] names)
        {
            var dt = CarContextExtentions.CreateDataTable("Name");
            foreach (var name in names)
            {
                dt.Rows.Add(name);
            }
            return dt;
        }
    }
}
