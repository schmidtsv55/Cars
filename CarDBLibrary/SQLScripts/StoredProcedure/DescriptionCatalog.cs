using System;
using System.Collections.Generic;
using System.Text;

namespace CarDBLibrary.SQLScripts.StoredProcedure
{
    public static class DescriptionCatalog
    {
		public const string ProcFindOrCreateDescriptionCatalog =
           @"IF EXISTS(SELECT 1 FROM sys.procedures 
                        WHERE object_id = OBJECT_ID(N'[dbo].[FindOrCreateDescriptionCatalog]'))
              BEGIN
                  RETURN
              END
              EXECUTE sp_executesql N'
                CREATE PROCEDURE [dbo].[FindOrCreateDescriptionCatalog]
                @DescriptionName nvarchar(250)                
                AS
				DECLARE @Id uniqueidentifier
                SELECT TOP 1 @Id = Id
	                FROM [dbo].[DescriptionCatalog]
	                WHERE ISNULL(Name, '''') = ISNULL(@DescriptionName, '''')
	            IF @Id IS NULL
	            BEGIN
                    SET @Id = NEWID();
	                  INSERT INTO [dbo].[DescriptionCatalog]
                             ([Id],
                             [Name])
                       VALUES
                             (@Id,
	    	                 @DescriptionName)
                END

				SELECT *
				    FROM [dbo].[DescriptionCatalog]
				    WHERE Id = @Id
              '";
        public static string[] ProcForDescriptionCatalog
        {
            get
            {
                return
                    new string[]
                    {
                        ProcFindOrCreateDescriptionCatalog
                    };
            }
        }
    }
}
