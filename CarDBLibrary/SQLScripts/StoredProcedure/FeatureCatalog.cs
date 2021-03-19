using System;
using System.Collections.Generic;
using System.Text;

namespace CarDBLibrary.SQLScripts.StoredProcedure
{
    public static class FeatureCatalog
    {
        public const string ProcFindOrCreateFeatureCatalog =
		   @"IF EXISTS(SELECT 1 FROM sys.procedures 
                        WHERE object_id = OBJECT_ID(N'[dbo].[FindOrCreateFeatureCatalog]'))
              BEGIN
                  RETURN
              END
              EXECUTE sp_executesql N'
               CREATE PROCEDURE [dbo]. [FindOrCreateFeatureCatalog]
				@DescriptionCatalogId uniqueidentifier,
				@Name nvarchar(250),
				@Standart bit,
				@Value nvarchar(250)
				AS
				BEGIN
				    DECLARE @Id uniqueidentifier
					DECLARE @FeatureNameCatalogId uniqueidentifier

					SELECT @FeatureNameCatalogId = Id
					    FROM [dbo].[FeatureNameCatalog]
						WHERE ISNULL(Name, '''') = ISNULL(@Name, '''')
					  
                    IF @FeatureNameCatalogId IS NULL
					BEGIN
						SET @FeatureNameCatalogId = NEWID()

						INSERT INTO [dbo].[FeatureNameCatalog] 
							(Id, Name)
							VALUES (@FeatureNameCatalogId, @Name)
					END		 

					SELECT @Id = Id 
						FROM [dbo].[FeatureCatalog] 
						WHERE DescriptionCatalogId = @DescriptionCatalogId 
						AND FeatureNameCatalogId = @FeatureNameCatalogId						
						AND 
						(    
						    (Standart IS NULL AND @Standart IS NULL)
						        OR
						    Standart = @Standart
						)
						AND ISNULL(Value, 0) = ISNULL(@Value, 0)
					IF @Id IS NULL
					BEGIN
						SET @Id = NEWID()

						INSERT INTO [dbo].FeatureCatalog 
							(Id, DescriptionCatalogId, FeatureNameCatalogId, Standart, Value)
							VALUES (@Id, @DescriptionCatalogId, @FeatureNameCatalogId, @Standart, @Value)
					END					

					SELECT *
					    FROM [dbo].[FeatureCatalog] 
						WHERE @Id = Id
				END
              '";
        public static string[] ProcForFeatureCatalog
		{
            get
            {
                return
                    new string[]
                    {
                        ProcFindOrCreateFeatureCatalog
                    };
            }
        }
    }
}
