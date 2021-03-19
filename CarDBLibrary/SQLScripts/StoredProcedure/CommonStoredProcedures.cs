using System;
using System.Collections.Generic;
using System.Text;

namespace CarDBLibrary.SQLScripts.StoredProcedure
{
    public static class CommonStoredProcedures
    {
        //Создание процедур для бызы
        public static IEnumerable<string> CreateCommonStoreProcedures 
        {
            get 
            {
                var storeProcedures = new List<string>();
                storeProcedures.AddRange(StoredProcedure.DescriptionCatalog.ProcForDescriptionCatalog);
                storeProcedures.AddRange(StoredProcedure.EquipmentCatalog.ProcForEquipmentsCatalog);
                storeProcedures.AddRange(StoredProcedure.EquipmentVersionCatalog.ProcForEquipmentVersionCatalog);
                storeProcedures.AddRange(StoredProcedure.FeatureCatalog.ProcForFeatureCatalog);
                storeProcedures.AddRange(StoredProcedure.InclusionCatalog.ProcForInclusionCatalog);
                storeProcedures.AddRange(StoredProcedure.MakeCatalog.ProcForMakeCatalog);
                storeProcedures.AddRange(StoredProcedure.ModelCatalog.ProcForModelCatalog);
                storeProcedures.AddRange(StoredProcedure.SpecificationCatalog.ProcForSpecificationCatalog);
                storeProcedures.AddRange(StoredProcedure.SpecificationInclusionCatalog.ProcForSpecificationInclusionCatalog);
                storeProcedures.AddRange(StoredProcedure.VersionCatalog.ProcForVersionCatalog);
                storeProcedures.AddRange(StoredProcedure.ModificationCatalog.ProcForModificationCatalog);
                storeProcedures.AddRange(StoredProcedure.ColorCatalog.ProcForColorCatalog);
                storeProcedures.AddRange(StoredProcedure.PricelistCatalog.ProcForPricelistCatalog);

                return storeProcedures;
            }
        }
    }
}
