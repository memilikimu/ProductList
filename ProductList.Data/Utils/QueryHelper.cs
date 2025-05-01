using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductList.Data.Utils
{
    public static class QueryHelper
    {
        public static async Task CommonExecNonQuery<T>(string constring, string query, SqlParameter[]? param)
        {
            var cmd = new SqlCommand(constring);
            await cmd.ExecuteNonQueryAsync();
        }
        private static async Task<T> CommonReadQuery<T>(string constring, string query, Func<SqlDataReader, Task<T>> method, SqlParameter[]? param)
        {
            var cmd = await CommonQuery(constring, query, param);
            using var reader = await cmd.ExecuteReaderAsync();
            return await method(reader);
        }

        public static async Task<SqlCommand> CommonQuery(string constring, string query, SqlParameter[]? param)
        {
            using var conn = new SqlConnection(constring);
            using var cmd = new SqlCommand(query, conn);
            if (param != null)
                cmd.Parameters.Add(param);
            await conn.OpenAsync();
            return cmd;
        }
    }
}
