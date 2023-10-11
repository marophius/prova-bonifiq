using ProvaPub.Models;

namespace ProvaPub.Interfaces
{
    public interface ICustomerService :IService<Customer>
    {
        Task<bool> CanPurchase(int customerId, decimal purchaseValue);
    }
}
