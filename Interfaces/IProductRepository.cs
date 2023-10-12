using ProvaPub.Models;

namespace ProvaPub.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProductsPagedList(int startIndex, int pageSize);
    }
}
