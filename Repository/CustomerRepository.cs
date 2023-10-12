using Microsoft.EntityFrameworkCore;
using ProvaPub.Interfaces;
using ProvaPub.Models;

namespace ProvaPub.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly TestDbContext _context;

        public CustomerRepository(TestDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CustomerExists(int id) => await _context.Customers.AnyAsync(x => x.Id == id);

        public async Task<List<Customer>> GetCustomersPagedList(int page, int pageSize) => await _context.Customers
                                                                                                                .Skip(page)
                                                                                                                .Take(pageSize)
                                                                                                                .ToListAsync();

        public async Task<int> HaveBoughtBefore(int id) => await _context.Customers.CountAsync(s => s.Id == id && s.Orders.Any());
    }
}
