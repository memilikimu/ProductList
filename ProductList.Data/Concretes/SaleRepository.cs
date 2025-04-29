using Microsoft.EntityFrameworkCore;
using ProductList.Data.Contexts;
using ProductList.Data.Entities;
using ProductList.Data.Interfaces;

namespace ProductList.Data.Concretes
{
    public class SaleRepository:Repository<Sale>, ISaleRepository
    {
        public SaleRepository(AppDbContext context):base(context){}
    }
}
