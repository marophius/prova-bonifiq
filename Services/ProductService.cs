using Microsoft.EntityFrameworkCore;
using ProvaPub.Interfaces;
using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
	public class ProductService : IProductService
	{
		TestDbContext _ctx;

		public ProductService(TestDbContext ctx)
		{
			_ctx = ctx;
		}

		public async Task<ModelList<Product>>  GetList(int page)
		{
			var productList = new ProductList();
			int startIndex = (page - 1) * productList.TotalCount;
			productList.Items = await _ctx.Products.Skip(startIndex).Take(productList.TotalCount).ToListAsync();

			return productList;
            //return new ProductList() {  HasNext=false, TotalCount =10, Products = _ctx.Products.ToList() };
        }

	}
}
