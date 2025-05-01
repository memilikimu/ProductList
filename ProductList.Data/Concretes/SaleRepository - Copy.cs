using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProductList.Data.Contexts;
using ProductList.Data.Entities;
using ProductList.Data.Interfaces;
using ProductList.Data.Utils;
using System.Runtime.CompilerServices;

namespace ProductList.Data.Concretes
{
    public abstract class SaleRepository:ISaleRepository
    {
        private readonly string _connectionString;
        public SaleRepository() { }
        public SaleRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }
        public async Task<IEnumerable<Sale>> GetAllAsync(string search, int page, int size)
        {

            var sales = new List<Sale>();

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_GetAllSale", conn);
            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                sales.Add(new Sale
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Product = reader.GetString(reader.GetOrdinal("Product")),
                    Price = reader.GetInt32(reader.GetOrdinal("Price")),
                    Amount = reader.GetInt32(reader.GetOrdinal("Amount")),
                    SellPrice = reader.GetInt32(reader.GetOrdinal("SellPrice")),
                    SellAmount = reader.GetInt32(reader.GetOrdinal("SellAmount"))
                });
            }

            return sales;
        }

        public async Task<int> GetCountAsync(string search)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_GetCountSale", conn);
            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return reader.GetInt32(0);
            }

            return 0;
        }

        public async Task<Sale> GetByIdAsync(object id)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_GetSaleById", conn);
            cmd.Parameters.AddWithValue("@Id", id);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new Sale
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Product = reader.GetString(reader.GetOrdinal("Product")),
                    Price = reader.GetInt32(reader.GetOrdinal("Price")),
                    Amount = reader.GetInt32(reader.GetOrdinal("Amount")),
                    SellPrice = reader.GetInt32(reader.GetOrdinal("SellPrice")),
                    SellAmount = reader.GetInt32(reader.GetOrdinal("SellAmount"))
                };
            }

            return null;
        }
        public async Task AddAndSaveAsync(Sale model)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_InsertSale", conn);

            cmd.Parameters.AddWithValue("@Product", model.Product ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Price", model.Price);
            cmd.Parameters.AddWithValue("@Amount", model.Amount);
            cmd.Parameters.AddWithValue("@SellPrice", model.SellPrice);
            cmd.Parameters.AddWithValue("@SellAmount", model.SellAmount);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }
        public async Task UpdateAndSaveAsync(Sale model)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_UpdateSale", conn);

            cmd.Parameters.AddWithValue("@Id", model.Product ?? (object)DBNull.Value);//dasds
            cmd.Parameters.AddWithValue("@Product", model.Product ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Price", model.Price);
            cmd.Parameters.AddWithValue("@Amount", model.Amount);
            cmd.Parameters.AddWithValue("@SellPrice", model.SellPrice);
            cmd.Parameters.AddWithValue("@SellAmount", model.SellAmount);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }
        public async Task DeleteAndSaveAsync(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_DeleteSale", conn);

            cmd.Parameters.AddWithValue("@Id", id);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }
    }
}
