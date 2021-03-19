using System;
using System.Collections.Generic;
using System.Text;

namespace CarDBLibrary.SQLScripts.StoredProcedure
{
    public static class MakeCatalog
    {
        public const string TypeMakeType =
            @"IF TYPE_ID(N'[dbo].[MakeType]') IS NOT NULL 
              BEGIN
                  RETURN
              END

              CREATE TYPE [dbo].[MakeType] AS TABLE(
	              [Name] [nvarchar](250) NULL
              )";

        public const string ProcCreateMakeCatalog =
            @"IF EXISTS(SELECT 1 FROM sys.procedures 
                        WHERE object_id = OBJECT_ID(N'[dbo].[CreateMakeCatalog]'))
              BEGIN
                  RETURN
              END
              EXECUTE sp_executesql N'
              CREATE PROCEDURE [dbo].[CreateMakeCatalog]
                  @Name nvarchar(50),  
                  @Id uniqueidentifier output
              AS    
                  SET @Id = NEWID();
	              INSERT INTO [dbo].[MakeCatalog]
                         ([Id],
                         [Name])
                   VALUES
                         (@Id,
	    	             @Name)
             '";

        public const string ProcFindOrCreateMakeCatalogId =
            @"IF EXISTS(SELECT 1 FROM sys.procedures 
                        WHERE object_id = OBJECT_ID(N'[dbo].[FindOrCreateMakeCatalogId]'))
              BEGIN
                  RETURN
              END
              EXECUTE sp_executesql N'
              CREATE PROCEDURE [dbo].[FindOrCreateMakeCatalogId]
                @MakeName nvarchar(50),  
                @Id uniqueidentifier output
              AS
                SELECT TOP 1 @Id = Id
	                FROM [dbo].[MakeCatalog]
	                WHERE Name = @MakeName
	            IF @Id IS NULL
	            BEGIN
                    SET @Id = NEWID();
	                  INSERT INTO [dbo].[MakeCatalog]
                             ([Id],
                             [Name])
                       VALUES
                             (@Id,
	    	                 @MakeName)
                END
              '";

        public const string ProcCreateMakesCatalog =
            @"IF EXISTS(SELECT 1 FROM sys.procedures 
                        WHERE object_id = OBJECT_ID(N'[dbo].[CreateMakeCatalog]'))
              BEGIN
                  RETURN
              END
              
              EXECUTE sp_executesql N'
              CREATE PROCEDURE [dbo].[CreateMakeCatalog]
                  @Makes [dbo].[MakeType] READONLY
              AS    
               BEGIN

              INSERT INTO [dbo].[MakeCatalog] ([Name])   
			  SELECT NAME
		      FROM @Makes
			  EXCEPT
			  SELECT NAME
			  FROM [dbo].[MakeCatalog]

              END
              '";


        public static string[] ProcForMakeCatalog
        {
            get 
            {
                return
                    new string[]
                    {
                        //TypeMakeType,
                        //ProcCreateMakeCatalog,
                        ProcFindOrCreateMakeCatalogId,
                        //ProcCreateMakes
                    };
            }
        }

    }
}
