using System;
using System.Collections.Generic;
using System.Text;

namespace CarDBLibrary.SQLScripts.StoredProcedure
{
    public static class ModelCatalog
    {
        public const string TypeModelType =
           @"IF TYPE_ID(N'[dbo].[ModelType]') IS NOT NULL 
              BEGIN
                  RETURN
              END

              CREATE TYPE [dbo].[ModelType] AS TABLE(
                  [MakeName] [nvarchar](250) NULL,
	              [ModelName] [nvarchar](250) NULL
                  
              )";
        public const string ProcCreateModels =
            @"IF EXISTS(SELECT 1 FROM sys.procedures 
                        WHERE object_id = OBJECT_ID(N'[dbo].[CreateModelsCatalog]'))
              BEGIN
                  RETURN
              END
              
              EXECUTE sp_executesql N'
              CREATE PROCEDURE [dbo].[CreateModelsCatalog]
                  @Models [dbo].[ModelType] READONLY
              AS    
               BEGIN

              INSERT INTO [dbo].[ModelsCatalog] ([Name], MakeId)   

			  (SELECT model.ModelName, makes.Id
		           FROM @Models model
			       INNER JOIN [dbo].MakesCatalog makes ON model.MakeName = makes.Name

			   EXCEPT

			   SELECT NAME, MakeId
			        FROM [dbo].[ModelsCatalog])

              END
              '";
        public const string ProcFindOrCreateModelCatalogId =
            @"IF EXISTS(SELECT 1 FROM sys.procedures 
                        WHERE object_id = OBJECT_ID(N'[dbo].[FindOrCreateModelCatalogId]'))
              BEGIN
                  RETURN
              END
			  EXECUTE sp_executesql N'
			  CREATE PROCEDURE [dbo].[FindOrCreateModelCatalogId]
                @MakeName nvarchar(100),
				@ModelName nvarchar(100),
                @Id uniqueidentifier output
              AS
			    DECLARE @MakeId uniqueidentifier
				EXEC [dbo].[FindOrCreateMakeCatalogId] @MakeName, @MakeId output

                SELECT TOP 1 @Id = Id
	                FROM [dbo].[ModelCatalog]
	                WHERE Name = @ModelName
					AND MakeCatalogId = @MakeId
	            IF @Id IS NULL
				BEGIN
				    SET @Id = NEWID();
				    INSERT INTO [dbo].[ModelCatalog] (Id, MakeCatalogId, Name)
					VALUES (@Id, @MakeId,@ModelName)
				END
				'";
        public static string[] ProcForModelCatalog
        {
            get
            {
                return
                    new string[]
                    {
                        //TypeModelType,
                        //ProcCreateModels
                        ProcFindOrCreateModelCatalogId
                    };
            }
        }
    }
}
