using System;
using System.Collections.Generic;
using System.Text;

namespace CarDBLibrary.SQLScripts.StoredProcedure
{
    public static class EquipmentVersionCatalog
    {
        public const string ProcFindOrCreateEquipmentVersionCatalog =
		   @"IF EXISTS(SELECT 1 FROM sys.procedures 
                        WHERE object_id = OBJECT_ID(N'[dbo].[FindOrCreateEquipmentVersionCatalog]'))
              BEGIN
                  RETURN
              END
              EXECUTE sp_executesql N'
               CREATE PROCEDURE [dbo]. [FindOrCreateEquipmentVersionCatalog]
				@VersionCatalogId uniqueidentifier,
				@EquipmentCatalogId uniqueidentifier				
				AS
				BEGIN
				    DECLARE @Id uniqueidentifier

					UPDATE [dbo].[EquipmentVersionCatalog]
					SET Status = ''Deactive''
					WHERE EquipmentCatalogId = @EquipmentCatalogId 
						AND VersionCatalogId != @VersionCatalogId
						AND ISNULL(Status, '''') != ''Deactive''

					SELECT @Id = Id 
						FROM [dbo].[EquipmentVersionCatalog] 
						WHERE EquipmentCatalogId = @EquipmentCatalogId 
						AND VersionCatalogId = @VersionCatalogId

					IF @Id IS NULL
					BEGIN
						SET @Id = NEWID()

						INSERT INTO [dbo].[EquipmentVersionCatalog] 
							(Id, EquipmentCatalogId, VersionCatalogId)
							VALUES (@Id, @EquipmentCatalogId, @VersionCatalogId)
					END				

					SELECT *
					    FROM [dbo].[EquipmentVersionCatalog] 
						WHERE @Id = Id
				END
              '";
		public static string[] ProcForEquipmentVersionCatalog
		{
			get
			{
				return
					new string[]
					{
						ProcFindOrCreateEquipmentVersionCatalog
					};
			}
		}
	}
}
