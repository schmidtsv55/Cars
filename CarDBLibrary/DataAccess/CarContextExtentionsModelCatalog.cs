using CarDBLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDBLibrary.DataAccess
{
    public static class CarContextExtentionsModelCatalog
    {

        public static async Task<Models.ModelCatalog> FindOrCreateModelCatalogAsync
            (
            this CarContext carContext, 
            string makeName,
            string modelName)
        {
            var query = @"EXECUTE [dbo].[FindOrCreateModelCatalog] 
                           @MakeName
                          ,@ModelName
                          ,@Id OUTPUT";
            var parMakeName = CarContextExtentions.CreateParNVarchar("@MakeName", makeName, 255);
            var parModelName = CarContextExtentions.CreateParNVarchar("@ModelName", modelName, 255);
            var parId = CarContextExtentions.ParIdOutput();
            carContext.Database.ExecuteSqlRaw(
                query,
                parMakeName,
                parModelName,
                parId);

            var model = new Models.ModelCatalog
            {
                Id = (Guid)parId.Value,
                Name = modelName
            };
            return model;
        }
        public static async Task CreateMakesAsync(this CarContext carContext, params Tuple<string, string>[] rows)
        {
            var query = @"EXECUTE [dbo].[CreateModels] 
                          @Models";
            carContext.Database.ExecuteSqlRaw(
                query,
                ParTableModels(rows));

        }
        private static Microsoft.Data.SqlClient.SqlParameter ParTableModels(params Tuple<string, string>[] rows)
        {
            return new Microsoft.Data.SqlClient.SqlParameter
            {
                ParameterName = "@Models",
                SqlDbType = SqlDbType.Structured,
                Value = CreateModelsTable(rows),
                TypeName = "[dbo].[ModelType]"
            };
        }
        private static DataTable CreateModelsTable(params Tuple<string, string>[] rows)
        {
            var dt = CarContextExtentions.CreateDataTable
                (
                    "MakeName",
                    "ModelName"                    
                );
            foreach (var row in rows)
            {
                dt.Rows.Add(row.Item1, row.Item2);
            }
            return dt;
        }

    }
}
