using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ProductList.Data.Interfaces;
using System.Data;

namespace ProductList.Data.Concretes
{
    public abstract class CustomConnection<Tmodel> : ICustomConnection<Tmodel>
    {

        protected readonly string _connectionString;
        public CustomConnection(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DbConnection")??"";
        }

        public virtual async Task<Tmodel?> CommonModelReader(string query, Func<SqlDataReader, Tmodel> mapper, SqlParameter[]? param)
        {
            using (var conn = new SqlConnection(_connectionString))
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

        public virtual async Task<List<Tmodel>> CommonModelsReader(string query, Func<SqlDataReader, Tmodel> mapper, SqlParameter[]? param)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (param != null)
                        cmd.Parameters.AddRange(param);
                    await conn.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        List<Tmodel> list = new List<Tmodel>();
                        while (await reader.ReadAsync())
                        {
                            list.Add(mapper(reader));
                        }
                        return list;
                    }
                }
            }

        }

        public virtual async Task CommonExecNonQuery(string query, SqlParameter[]? param)
        {
            using (var conn = new SqlConnection(_connectionString))
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
