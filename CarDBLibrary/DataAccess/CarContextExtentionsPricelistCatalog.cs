using CarDBLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDBLibrary.DataAccess
{
    public static class CarContextExtentionsPricelistCatalog
    {

        public static async Task<PricelistCatalog> FindOrCreatePricelistCatalogAsync
            (
                this CarContext carContext, 
                string colorName,
                bool? metallic,
                string hue,                
                string picture,
                string price,
                DateTime? productionFromDate,
                DateTime? validFromDate,
                Guid equipmentCatalogId
            )
        {
            var query = @"EXECUTE [dbo].[FindOrCreatePricelistCatalog] 
                        @ColorName,  
				        @Metallic,
				        @Hue,
                        @Picture,
				        @Price,
				        @ProductionFromDate,
				        @ValidFromDate,
                        @EquipmentCatalogId";

            var parColorName = CarContextExtentions.CreateParNVarchar("@ColorName", colorName, 250);
            var parMet = CarContextExtentions.CreateParBit("@Metallic",  metallic);
            var parHue = CarContextExtentions.CreateParNVarchar("@Hue", hue, 250);
            var parPicture = CarContextExtentions.CreateParNVarchar("@Picture", picture, 250);
            var parPrice = CarContextExtentions.CreateParNVarchar("@Price", price, 20);
            var parProductFromDate = CarContextExtentions.CreateParDateTime2("@ProductionFromDate", productionFromDate);
            var parValidFromDate = CarContextExtentions.CreateParDateTime2("@ValidFromDate", validFromDate);
            var parEquipmentCatalogId = CarContextExtentions.CreateParGuid("@EquipmentCatalogId", equipmentCatalogId);
            return carContext.PricelistCatalog.FromSqlRaw
                (
                    query, 
                    parColorName,
                    parMet,
                    parHue,
                    parPrice,
                    parProductFromDate,
                    parValidFromDate,
                    parPicture,
                    parEquipmentCatalogId
                ).AsEnumerable().FirstOrDefault();
        }
    }
}
