using Microsoft.EntityFrameworkCore;
using ProvaPub.Interfaces;
using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;

        public CustomerService(
            ICustomerRepository customerRepository,
            IOrderRepository orderRepository)
        {
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
        }

        public async Task<ModelList<Customer>> GetList(int page)
        {
            var customerList = new CustomerList();
            int startIndex = (page - 1) * customerList.TotalCount;
            customerList.Items = await _customerRepository.GetCustomersPagedList(startIndex, page);

            return customerList;
        }

        public async Task<bool> CanPurchase(int customerId, decimal purchaseValue)
        {
            ValidateInput(customerId, purchaseValue);

            if(!CustomerExists(customerId))
                return false;

            if (!CustomerCanPurchaseThisMonth(customerId))
                return false;

            if(!CustomerIsEligibleForFirstPurchase(customerId, purchaseValue))
                return false;

            return true;
        }

        private void ValidateInput(int customerId, decimal purchaseValue)
        {
            if (customerId <= 0)
                throw new ArgumentOutOfRangeException(nameof(customerId));

            if (purchaseValue <= 0)
                throw new ArgumentOutOfRangeException(nameof(purchaseValue));
        }

        private bool CustomerExists(int customerId)
        {
            //Business Rule: Non registered Customers cannot purchase
            return _customerRepository.CustomerExists(customerId).Result;
        }

        private bool CustomerCanPurchaseThisMonth(int customerId)
        {
            //Business Rule: A customer can purchase only a single time per month
            var baseDate = DateTime.UtcNow.AddMonths(-1);
            var ordersInThisMonth = _orderRepository.CountOrdersInThisMonth(baseDate, customerId).Result;
            return ordersInThisMonth == 0;
        }

        private bool CustomerIsEligibleForFirstPurchase(int customerId, decimal purchaseValue)
        {
            //Business Rule: A customer that never bought before can make a first purchase of maximum 100,00
            var haveBoughtBefore = _customerRepository.HaveBoughtBefore(customerId).Result;
            return haveBoughtBefore > 0 || purchaseValue <= 100;
        }

    }
}
