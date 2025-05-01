using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ProductList.Data.Models;
using ProductList.Data.Interfaces;
using ProductList.Data.Utils;

namespace ProductList.Data.Concretes
{
    public class SaleRepository : CustomConnection<Sale>, ISaleRepository
    {
        public SaleRepository(IConfiguration configuration) : base(configuration) { }
        public async Task<IEnumerable<Sale>> GetAllAsync(string product, int page, int pageSize)
        {
            
            SqlParameter[] parameters = new SqlParameter[3] {
                new SqlParameter("@Product", product ?? (object)DBNull.Value),
                new SqlParameter("@PageSize", pageSize),
                new SqlParameter("@PageNumber", page)
            };
            
            //return await QueryHelper.CommonModelsReader(_connectionString, "sp_GetAllSale", SaleDbHelper.MapReaderToModel, parameters);
            return await CommonModelsReader("sp_GetAllSale", SaleDbHelper.MapReaderToModel, parameters);
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
            return await CommonModelReader("sp_GetSaleById", SaleDbHelper.MapReaderToModel, parameters);
        }

        public async Task AddAndSaveAsync(Sale model)
        {
            await CommonExecNonQuery("sp_InsertSale", SaleDbHelper.MapToParam(model, false));
        }
        public async Task UpdateAndSaveAsync(Sale model)
        {
            await CommonExecNonQuery("sp_UpdateSale", SaleDbHelper.MapToParam(model, true));
        }
        public async Task DeleteAndSaveAsync(int id)
        {
            SqlParameter[] parameters = new SqlParameter[1] {
                new SqlParameter("@Id", id)
            };
            await CommonExecNonQuery("sp_DeleteSale", parameters);
        }
    }
}
