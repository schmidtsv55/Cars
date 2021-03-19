using CarDBLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDBLibrary.DataAccess
{
    public static class CarContextExtentionsVersionCatalog
    {
        public static async Task<VersionCatalog> FindOrCreateVersionCatalog
            (
            this CarContext carContext,
            string makeName,
            string modelName,
            string versionName,
            string startProductionYear,
            string picture
            )
        {
            var query = @"EXECUTE [dbo].[FindOrCreateVersionCatalog] 
                               @MakeName
                              ,@ModelName
                              ,@VersionName
                              ,@StartProductionYear
                              ,@Picture";

            var makeNamePar =
                CarContextExtentions.CreateParNVarchar
                ("@MakeName", makeName, 250);

            var modelNamePar =
                CarContextExtentions.CreateParNVarchar
                ("@ModelName", modelName, 250);

            var versionNamePar =
                CarContextExtentions.CreateParNVarchar
                ("@VersionName", versionName, 250);

            var startProductionYearPar = 
                CarContextExtentions.CreateParNVarchar
                ("@StartProductionYear", startProductionYear, 4);

            var picturePar =
                  CarContextExtentions.CreateParNVarchar
                ("@Picture", picture, 250);
            return carContext.VersionCatalog.FromSqlRaw(query, makeNamePar, modelNamePar, versionNamePar, startProductionYearPar, picturePar).AsEnumerable().First();
        }

    }
}

