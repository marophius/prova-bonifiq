using ProvaPub.Models;

namespace ProvaPub.Interfaces
{
    public interface ICustomerRepository
    {
        Task<bool> CustomerExists(int id);
        Task<List<Customer>> GetCustomersPagedList(int startIndex, int pageSize);
        Task<int> HaveBoughtBefore(int id);
    }
}
