using Microsoft.Data.SqlClient;
using ProductList.Data.Entities;

namespace ProductList.Data.Utils
{
    public static class SaleDbHelper
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
                parameters[5] = new SqlParameter("@Id", model.Id);//dasds
            }
            return parameters;
        }

        public static Sale MapReaderToModel(SqlDataReader reader)
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
    }
}
