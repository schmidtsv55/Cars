using System;
using System.Collections.Generic;
using System.Text;

namespace CarDBLibrary.SQLScripts.StoredProcedure
{
    public static class InclusionCatalog
    {
		public const string ProcFindOrCreateInclusionCatalog =
		   @"IF EXISTS(SELECT 1 FROM sys.procedures 
                        WHERE object_id = OBJECT_ID(N'[dbo].[FindOrCreateInclusionCatalog]'))
              BEGIN
                  RETURN
              END
              EXECUTE sp_executesql N'
               CREATE PROCEDURE [dbo].[FindOrCreateInclusionCatalog]
				@Standart bit,
				@Description nvarchar(250)
				AS
				BEGIN
				    DECLARE @Id uniqueidentifier
									   
					SELECT @Id = Id 
						FROM [dbo].[InclusionCatalog] 
						WHERE
						(    
						    (Standart IS NULL AND @Standart IS NULL)
						        OR
						    Standart = @Standart
						)
						AND  ISNULL(Description, '''') = ISNULL(@Description, '''')

					IF @Id IS NULL
					BEGIN
						SET @Id = NEWID()

					    INSERT INTO [dbo].[InclusionCatalog] 
						(Id, Standart, Description)
						VALUES (@Id, @Standart, @Description)
					END

					

					SELECT *
					    FROM [dbo].[InclusionCatalog] 
						WHERE @Id = Id
				END
              '";
		public static string[] ProcForInclusionCatalog
		{
			get
			{
				return
					new string[]
					{
						ProcFindOrCreateInclusionCatalog
					};
			}
		}
	}
}
