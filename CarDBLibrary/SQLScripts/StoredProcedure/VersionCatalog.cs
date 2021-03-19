using System;
using System.Collections.Generic;
using System.Text;

namespace CarDBLibrary.SQLScripts.StoredProcedure
{
    public static class VersionCatalog
    {
		public const string ProcFindOrCreateVersionCatalog =
			@"IF EXISTS(SELECT 1 FROM sys.procedures 
                        WHERE object_id = OBJECT_ID(N'[dbo].[FindOrCreateVersionCatalog]'))
              BEGIN
                  RETURN
              END
			  EXECUTE sp_executesql N'
			   CREATE PROCEDURE [dbo].[FindOrCreateVersionCatalog]
                @MakeName nvarchar(250),
				@ModelName nvarchar(250),
				@VersionName nvarchar(250),
				@StartProductionYear nvarchar(20),
				@Picture nvarchar(250)
              AS
			    DECLARE @Id uniqueidentifier
			    DECLARE @ModificationId uniqueidentifier
				DECLARE @PictureVersion nvarchar(250)
				EXEC [dbo].[FindOrCreateModificationCatalogId] @MakeName, @ModelName,@StartProductionYear, @ModificationId output

                SELECT TOP 1 @Id = Id, @PictureVersion = Picture
	                FROM [dbo].[VersionCatalog]
	                WHERE Name = @VersionName
					AND ModificationCatalogId = @ModificationId

				IF @Id IS NOT NULL AND @PictureVersion != @Picture
				BEGIN
				    UPDATE [dbo].[VersionCatalog]
					SET Picture = @Picture
					WHERE Id = @Id
				END
	            IF @Id IS NULL
				BEGIN
				    SET @Id = NEWID();
				    INSERT INTO [dbo].[VersionCatalog] (Id, ModificationCatalogId, Name, Picture)
					VALUES (@Id, @ModificationId, @VersionName, @Picture)
				END 
				SELECT *
				FROM [dbo].[VersionCatalog]
				WHERE Id = @Id
				'";
        public static string[] ProcForVersionCatalog
        {
            get
            {
                return
                    new string[]
                    {
                        ProcFindOrCreateVersionCatalog
                    };
            }
        }
    }
}
