using System;
using System.Collections.Generic;
using System.Text;

namespace CarDBLibrary.SQLScripts.StoredProcedure
{
    public static class ColorCatalog
    {
        public const string ProcFindOrCreateColorCatalogId =
           @"IF EXISTS(SELECT 1 FROM sys.procedures 
                        WHERE object_id = OBJECT_ID(N'[dbo].[FindOrCreateColorCatalogId]'))
              BEGIN
                  RETURN
              END
              EXECUTE sp_executesql N'
              CREATE PROCEDURE [dbo].[FindOrCreateColorCatalogId]
                @Name nvarchar(250),  
				@Metallic bit,
				@Hue nvarchar(100),
                @Picture nvarchar(250),
                @Id uniqueidentifier output
              AS
                SELECT TOP 1 @Id = Id
	                FROM [dbo].[ColorCatalog]
	                WHERE ISNULL(Name, '''') = ISNULL(@Name, '''')
					AND  ISNULL(Hue, '''') = ISNULL(@Hue, '''')
					AND ISNULL(Metallic, 0) = ISNULL(@Metallic, 0) 
                    AND ISNULL(Picture, '''') = ISNULL(@Picture, '''') 
	            IF @Id IS NULL
	            BEGIN
                    SET @Id = NEWID();
	                  INSERT INTO [dbo].[ColorCatalog]
                             ([Id],
                             [Name],
							 [Metallic],
							 [Hue],
                             [Picture])
                       VALUES
                             (@Id,
	    	                 @Name,
							 @Metallic,
							 @Hue,
                             @Picture)
                END
              '";
        public static string[] ProcForColorCatalog
        {
            get
            {
                return
                    new string[]
                    {
                        ProcFindOrCreateColorCatalogId
                    };
            }
        }
    }
}
