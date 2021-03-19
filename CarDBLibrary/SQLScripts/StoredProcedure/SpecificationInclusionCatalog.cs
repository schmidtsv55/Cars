using System;
using System.Collections.Generic;
using System.Text;

namespace CarDBLibrary.SQLScripts.StoredProcedure
{
    public static class SpecificationInclusionCatalog
    {
		public const string ProcFindOrCreateFindOrCreateSpecificationInclusionCatalog =
		  @"IF EXISTS(SELECT 1 FROM sys.procedures 
                        WHERE object_id = OBJECT_ID(N'[dbo].[FindOrCreateSpecificationInclusionCatalog]'))
              BEGIN
                  RETURN
              END
              EXECUTE sp_executesql N'
               CREATE PROCEDURE [dbo].[FindOrCreateSpecificationInclusionCatalog]
				@InclusionCatalogId uniqueidentifier,
				@SpecificationCatalogId uniqueidentifier				
				AS
				BEGIN
				    DECLARE @Id uniqueidentifier

					SELECT @Id = Id 
						FROM [dbo].[SpecificationInclusionCatalog] 
						WHERE InclusionCatalogId = @InclusionCatalogId 
						AND SpecificationCatalogId = @SpecificationCatalogId

					IF @Id IS NULL
					BEGIN
						SET @Id = NEWID()

						INSERT INTO [dbo].[SpecificationInclusionCatalog] 
							(Id, InclusionCatalogId, SpecificationCatalogId)
							VALUES (@Id, @InclusionCatalogId, @SpecificationCatalogId)
					END					

					SELECT *
					    FROM [dbo].[SpecificationInclusionCatalog] 
						WHERE @Id = Id
				END
              '";
        public static string[] ProcForSpecificationInclusionCatalog
		{
			get
			{
				return
					new string[]
					{
						ProcFindOrCreateFindOrCreateSpecificationInclusionCatalog
					};
			}
		}
	}
}
