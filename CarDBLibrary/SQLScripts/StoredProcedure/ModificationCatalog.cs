using System;
using System.Collections.Generic;
using System.Text;

namespace CarDBLibrary.SQLScripts.StoredProcedure
{
    public static class ModificationCatalog
    {
		public const string ProcFindOrCreateModificationCatalog =
			@"IF EXISTS(SELECT 1 FROM sys.procedures 
                        WHERE object_id = OBJECT_ID(N'[dbo].[FindOrCreateModificationCatalogId]'))
              BEGIN
                  RETURN
              END

			  EXECUTE sp_executesql N'
			  CREATE PROCEDURE [dbo].[FindOrCreateModificationCatalogId]
                @MakeName nvarchar(100),
				@ModelName nvarchar(100),
				@StartProductionYear nvarchar(4),
                @Id uniqueidentifier output
              AS
			    DECLARE @ModelId uniqueidentifier
				EXEC [dbo].[FindOrCreateModelCatalogId] @MakeName, @ModelName, @ModelId output

                SELECT TOP 1 @Id = Id
	                FROM [dbo].[ModificationCatalog]
	                WHERE ISNULL(StartProductionYear, '''') = ISNULL(@StartProductionYear, '''')
					AND ModelCatalogId = @ModelId
	            IF @Id IS NULL
				BEGIN
				    SET @Id = NEWID();
				    INSERT INTO [dbo].[ModificationCatalog] (Id, ModelCatalogId, StartProductionYear)
					VALUES (@Id, @ModelId, @StartProductionYear)
				END 
				'";
		public static string[] ProcForModificationCatalog
		{
			get
			{
				return
					new string[]
					{
						ProcFindOrCreateModificationCatalog
					};
			}
		}
	}
}
