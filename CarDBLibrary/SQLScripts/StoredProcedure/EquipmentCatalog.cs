using System;
using System.Collections.Generic;
using System.Text;

namespace CarDBLibrary.SQLScripts.StoredProcedure
{
    public static class EquipmentCatalog
    {
        public const string ProcFindOrCreateEquipmentsCatalog =
            @"IF EXISTS(SELECT 1 FROM sys.procedures 
                        WHERE object_id = OBJECT_ID(N'[dbo].[FindOrCreateEquipmentCatalog]'))
              BEGIN
                  RETURN
              END
              EXECUTE sp_executesql N'
              CREATE PROCEDURE [dbo].[FindOrCreateEquipmentCatalog]
                @DatabaseId int 
                
              AS
			    DECLARE @Id uniqueidentifier
                SELECT TOP 1 @Id = Id
	                FROM [dbo].[EquipmentCatalog]
	                WHERE DatabaseId = @DatabaseId
	            IF @Id IS NULL
	            BEGIN
                    SET @Id = NEWID();
	                  INSERT INTO [dbo].[EquipmentCatalog]
                             ([Id],
                             [DatabaseId])
                       VALUES
                             (@Id,
	    	                 @DatabaseId)
                END
				SELECT *
				FROM [dbo].[EquipmentCatalog]
				WHERE Id = @Id
              '";
        public static string[] ProcForEquipmentsCatalog
        {
            get
            {
                return
                    new string[]
                    {
                        ProcFindOrCreateEquipmentsCatalog
                    };
            }
        }
    }
}
