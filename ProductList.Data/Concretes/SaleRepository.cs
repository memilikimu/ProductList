using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProductList.Data.Entities;
using ProductList.Data.Interfaces;
using ProductList.Data.Utils;

namespace ProductList.Data.Concretes
{
    public class SaleRepository : ISaleRepository
    {
        private readonly string _connectionString;
        public SaleRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DbConnection")!;
        }
        public async Task<IEnumerable<Sale>> GetAllAsync(string product, int page, int pageSize)
        {
            
            SqlParameter[] parameters = new SqlParameter[3] {
                new SqlParameter("@Product", product ?? (object)DBNull.Value),
                new SqlParameter("@PageSize", pageSize),
                new SqlParameter("@PageNumber", page)
            };
            
            return await QueryHelper.CommonModelsReader(_connectionString, "sp_GetAllSale", SaleDbHelper.MapReaderToModel, parameters);
        }

        public async Task<int> GetCountAsync(string product)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand("sp_GetCountSale", conn))
                {
                    cmd.Parameters.AddWithValue("@Product", product ?? (object)DBNull.Value);
                    await conn.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return reader.GetInt32(0);
                        }
                    }
                }
            }
            return 0;
        }

        public async Task<Sale> GetByIdAsync(object id)
        {
            SqlParameter[] parameters = new SqlParameter[1] {
                new SqlParameter("@Id", id)
            };
            return await QueryHelper.CommonModelReader(_connectionString, "sp_GetSaleById", SaleDbHelper.MapReaderToModel, parameters);
        }

        public async Task AddAndSaveAsync(Sale model)
        {
            await QueryHelper.CommonExecNonQuery(_connectionString, "sp_InsertSale", SaleDbHelper.MapToParam(model, false));
        }
        public async Task UpdateAndSaveAsync(Sale model)
        {
            await QueryHelper.CommonExecNonQuery(_connectionString, "sp_UpdateSale", SaleDbHelper.MapToParam(model, true));
        }
        public async Task DeleteAndSaveAsync(int id)
        {
            SqlParameter[] parameters = new SqlParameter[1] {
                new SqlParameter("@Id", id)
            };
            await QueryHelper.CommonExecNonQuery(_connectionString, "sp_DeleteSale", parameters);
        }
    }
}
