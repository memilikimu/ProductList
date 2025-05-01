using Microsoft.Data.SqlClient;

namespace ProductList.Data.Interfaces
{
    public interface ICustomConnection<Tmodel>
    {
        Task<Tmodel?> CommonModelReader(string query, Func<SqlDataReader, Tmodel> mapper, SqlParameter[]? param);
        Task<List<Tmodel>> CommonModelsReader(string query, Func<SqlDataReader, Tmodel> mapper, SqlParameter[]? param);
        Task CommonExecNonQuery(string query, SqlParameter[]? param);
    }
}
