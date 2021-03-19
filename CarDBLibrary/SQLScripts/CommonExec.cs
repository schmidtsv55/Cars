using System;
using System.Collections.Generic;
using System.Text;

namespace CarDBLibrary.SQLScripts
{
    public static class CommonExec
    {
        public static IEnumerable<string> CreateNecessarySQLObjects
        {
            get 
            {
                var storeProcedures = new List<string>();
                storeProcedures.AddRange(StoredProcedure.CommonStoredProcedures.CreateCommonStoreProcedures);
                return storeProcedures;                
            }
        }
    }
}
