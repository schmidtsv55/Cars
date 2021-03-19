using System;
using System.Collections.Generic;
using System.Text;

namespace CarDBLibrary.SQLScripts.StoredProcedure
{
    public static class PricelistCatalog
    {
		public const string ProcFindOrCreateFindOrCreatePricelistCatalog =
		   @"IF EXISTS(SELECT 1 FROM sys.procedures 
                        WHERE object_id = OBJECT_ID(N'[dbo].[FindOrCreatePricelistCatalog]'))
              BEGIN
                  RETURN
              END
			  EXECUTE sp_executesql N'

			  CREATE PROCEDURE [dbo].[FindOrCreatePricelistCatalog]
                @ColorName nvarchar(250),  
				@Metallic bit,
				@Hue nvarchar(250),
                @Picture nvarchar(250),
				@Price nvarchar(20),
				@ProductionFromDate DateTime,
				@ValidFromDate DateTime,
                @EquipmentCatalogId uniqueidentifier
              AS
			    DECLARE  @Id uniqueidentifier
			    DECLARE  @ColorId uniqueidentifier
				EXEC [dbo].[FindOrCreateColorCatalogId] @ColorName, @Metallic, @Hue, @Picture, @ColorId output

				UPDATE [dbo].[PricelistCatalog]
					SET Status = ''Deactive''
					WHERE EquipmentCatalogId = @EquipmentCatalogId 
						AND ColorCatalogId = @ColorId
						AND ISNULL(Status, '''') != ''Deactive''
						AND 
						(
						    ISNULL(Price, '''') != ISNULL(@Price, '''') OR
							ISNULL(ProductionFromDate, ''1900-01-01'') != ISNULL(@ProductionFromDate, ''1900-01-01'') OR
							ISNULL(ValidFromDate, ''1900-01-01'') != ISNULL(@ValidFromDate, ''1900-01-01'')
						)

                SELECT TOP 1 @Id = Id
	                FROM [dbo].[PricelistCatalog]
	                WHERE EquipmentCatalogId = @EquipmentCatalogId 
						AND ColorCatalogId = @ColorId
						AND ISNULL(Status, '''') = ''Active''
						AND ISNULL(Price, '''') = ISNULL(@Price, '''')
						AND	ISNULL(ProductionFromDate, ''1900-01-01'') = ISNULL(@ProductionFromDate, ''1900-01-01'')
						AND	ISNULL(ValidFromDate, ''1900-01-01'') = ISNULL(@ValidFromDate, ''1900-01-01'')

	            IF @Id IS NULL
	            BEGIN
                    SET @Id = NEWID();
	                  INSERT INTO [dbo].[PricelistCatalog]
                             ([EquipmentCatalogId],
                             [ColorCatalogId],
							 [Price],
							 [ProductionFromDate],
							 [ValidFromDate])
                       VALUES
                             (@EquipmentCatalogId
	    	                 ,@ColorId
							 ,@Price
							 ,@ProductionFromDate
							 ,@ValidFromDate)
                END

				SELECT *
				FROM [dbo].[PricelistCatalog]
				WHERE Id = @Id
				'";
		public static string[] ProcForPricelistCatalog
		{
			get
			{
				return
					new string[]
					{
						ProcFindOrCreateFindOrCreatePricelistCatalog
					};
			}
		}
	}
}
