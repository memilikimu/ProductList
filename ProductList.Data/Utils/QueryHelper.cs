using Microsoft.Data.SqlClient;
using ProductList.Data.Entities;
using System.Data;
namespace ProductList.Data.Utils
{
    public static class QueryHelper
    {
        public static async Task<T?> CommonModelReader<T>(string constring, string query, Func<SqlDataReader, T> mapper, SqlParameter[]? param)
        {
            using (var conn = new SqlConnection(constring))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (param != null)
                        cmd.Parameters.AddRange(param);
                    await conn.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return mapper(reader);
                        }
                        return default;
                    }
                }
            }
        }

        public static async Task<List<T>> CommonModelsReader<T>(string constring, string query,  Func<SqlDataReader, T> mapper, SqlParameter[]? param)
        {
            using (var conn = new SqlConnection(constring))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (param != null)
                        cmd.Parameters.AddRange(param);
                    await conn.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        List<T> list = new List<T>();
                        while (await reader.ReadAsync())
                        {
                            list.Add(mapper(reader));
                        }
                        return list;
                    }
                }
            }

        }

        public static async Task CommonExecNonQuery(string constring, string query, SqlParameter[]? param)
        {
            using (var conn = new SqlConnection(constring))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (param != null)
                        cmd.Parameters.AddRange(param);
                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }

            }

        }
    }
}
