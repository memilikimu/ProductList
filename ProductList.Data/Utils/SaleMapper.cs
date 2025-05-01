using Microsoft.Data.SqlClient;
using ProductList.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProductList.Data.Utils
{
    public static class SaleMapper
    {
        public static SqlParameter[] MapToParam(Sale model, bool isUpdate)
        {
            int count = isUpdate ? 6 : 5;
            SqlParameter[] parameters = new SqlParameter[count];
            parameters[0] = new SqlParameter("@Product", model.Product ?? (object)DBNull.Value);
            parameters[1] = new SqlParameter("@Price", model.Price);
            parameters[2] = new SqlParameter("@Amount", model.Amount);
            parameters[3] = new SqlParameter("@SellPrice", model.SellPrice);
            parameters[4] = new SqlParameter("@SellAmount", model.SellAmount);
            if (isUpdate)
            {
                parameters[5] = new SqlParameter("@Id", model.Product ?? (object)DBNull.Value);//dasds
            }
            return parameters;
        }
        public static async Task<List<Sale>> MapToModel(this SqlDataReader reader)
        {
            var sales = new List<Sale>();
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
    }
}
