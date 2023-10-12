using Microsoft.EntityFrameworkCore;
using ProvaPub.Interfaces;
using ProvaPub.Models;

namespace ProvaPub.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly TestDbContext _context;

        public ProductRepository(TestDbContext context)
        {
            _context = context;
        }
        public async Task<List<Product>> GetProductsPagedList(int startIndex, int pageSize) => await _context.Products.Skip(startIndex).Take(pageSize).ToListAsync();
    }
}
