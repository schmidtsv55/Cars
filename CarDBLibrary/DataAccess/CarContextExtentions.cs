using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CarDBLibrary.DataAccess
{
    public static class CarContextExtentions
    {
        
        public static Microsoft.Data.SqlClient.SqlParameter ParNameInput(string name) 
        {
            return new Microsoft.Data.SqlClient.SqlParameter
            {
                ParameterName = "@name",
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input,
                Size = 250,
                Value = (object)name ?? DBNull.Value
            };
        }
        public static Microsoft.Data.SqlClient.SqlParameter ParDatabaseIdInput(int databaseId)
        {
            return CarContextExtentions.CreateParInt("@DatabaseId", databaseId);
        }
        public static Microsoft.Data.SqlClient.SqlParameter CreateParBit
            (
            string parName, bool? parValue
            )
        {
            return new Microsoft.Data.SqlClient.SqlParameter
            {
                ParameterName = parName,
                SqlDbType = SqlDbType.Bit,
                Direction = ParameterDirection.Input,
                Value = (object)parValue ?? DBNull.Value,
            };
        }
        public static Microsoft.Data.SqlClient.SqlParameter CreateParDateTime2
            (
            string parName, DateTime? parValue
            )
        {
            return new Microsoft.Data.SqlClient.SqlParameter
            {
                ParameterName = parName,
                SqlDbType = SqlDbType.DateTime2,
                Direction = ParameterDirection.Input,
                Value = (object)parValue ?? DBNull.Value,
            };
        }
        public static Microsoft.Data.SqlClient.SqlParameter CreateParNVarchar
            (
            string parName, string parValue, int size
            ) 
        {
            return new Microsoft.Data.SqlClient.SqlParameter
            {
                ParameterName = parName,
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input,
                Size = size,
                Value = (object)parValue ?? DBNull.Value,
            };
        }
        public static Microsoft.Data.SqlClient.SqlParameter CreateParInt
            (
            string parName, int? parValue
            )
        {
            return new Microsoft.Data.SqlClient.SqlParameter
            {
                ParameterName = parName,
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                Value = (object)parValue ?? DBNull.Value,
            };
        }
        public static Microsoft.Data.SqlClient.SqlParameter CreateParGuid
            (
            string parName, Guid parValue
            )
        {
            return new Microsoft.Data.SqlClient.SqlParameter
            {
                ParameterName = parName,
                SqlDbType = SqlDbType.UniqueIdentifier,
                Direction = ParameterDirection.Input,
                Value = parValue
            };
        }
        public static Microsoft.Data.SqlClient.SqlParameter ParIdOutput()
        {
            return new Microsoft.Data.SqlClient.SqlParameter
            {
                ParameterName = "@Id",
                SqlDbType = SqlDbType.UniqueIdentifier,
                Direction = ParameterDirection.Output
            };
        }
        

        public static DataTable CreateDataTable(params string[] columns) 
        {
            var dt = new DataTable();
            dt.Columns.AddRange(columns.Select(x => new DataColumn(x)).ToArray());
            return dt;
        }
    }
}
