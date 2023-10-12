using Microsoft.EntityFrameworkCore;
using ProvaPub.Interfaces;

namespace ProvaPub.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly TestDbContext _context;

        public OrderRepository(TestDbContext context)
        {
            _context = context;
        }
        public async Task<int> CountOrdersInThisMonth(DateTime date, int customerId) => await _context.Orders
                                                                                                        .CountAsync(s => s.CustomerId == customerId && s.OrderDate >= date);
    }
}
