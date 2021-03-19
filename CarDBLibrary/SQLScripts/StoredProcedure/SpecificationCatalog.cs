using System;
using System.Collections.Generic;
using System.Text;

namespace CarDBLibrary.SQLScripts.StoredProcedure
{
    public static class SpecificationCatalog
    {
		public const string ProcFindOrCreateFindOrCreateSpecificationCatalog =
		  @"IF EXISTS(SELECT 1 FROM sys.procedures 
                        WHERE object_id = OBJECT_ID(N'[dbo].[FindOrCreateSpecificationCatalog]'))
              BEGIN
                  RETURN
              END
              EXECUTE sp_executesql N'
               CREATE PROCEDURE [dbo].[FindOrCreateSpecificationCatalog]
				@EquipmentCatalogId uniqueidentifier,
				@FeatureCatalogId uniqueidentifier				
				AS
				BEGIN
				    DECLARE @Id uniqueidentifier

					SELECT @Id = Id 
						FROM [dbo].[SpecificationCatalog] 
						WHERE EquipmentCatalogId = @EquipmentCatalogId 
						AND FeatureCatalogId = @FeatureCatalogId
                        AND Status IN (''Active'', ''Updating'')

					IF @Id IS NULL
					BEGIN
						SET @Id = NEWID()

						INSERT INTO [dbo].[SpecificationCatalog] 
							(Id, EquipmentCatalogId, FeatureCatalogId, Status)
							VALUES (@Id, @EquipmentCatalogId, @FeatureCatalogId, ''Updating'')
					END					

					SELECT *
					    FROM [dbo].[SpecificationCatalog] 
						WHERE @Id = Id
				END
              '";
		public const string ProcActivateNewSpecificationCatalog =
			@"IF EXISTS(SELECT 1 FROM sys.procedures 
                        WHERE object_id = OBJECT_ID(N'[dbo].[ActivateNewSpecificationCatalog]'))
              BEGIN
                  RETURN
              END
              EXECUTE sp_executesql N'
              CREATE PROCEDURE [dbo].[ActivateNewSpecificationCatalog]
                @EquipmentCatalogId uniqueidentifier 
                
              AS
			   UPDATE [dbo].[SpecificationCatalog]
			   SET Status = 
			   CASE 
				  WHEN Status = ''Updating''
				  THEN ''Active''
				  ELSE ''Deactive''
			   END
			   WHERE EquipmentCatalogId = @EquipmentCatalogId
			   AND Status != ''Deactive''
              '";
        public static string[] ProcForSpecificationCatalog => new string[]
                    {
                        ProcFindOrCreateFindOrCreateSpecificationCatalog,
						ProcActivateNewSpecificationCatalog
					};
    }
}
