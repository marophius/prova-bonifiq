
using ProvaPub.Interfaces;
using ProvaPub.Models;

namespace ProvaPub.Services
{
	public class OrderService
	{
		private readonly IPaymentMethodFactory _factory;

        public OrderService(IPaymentMethodFactory factory)
        {
            _factory = factory;
        }
        public async Task<Order> PayOrder(string paymentMethod, decimal paymentValue, int customerId)
		{
			IPaymentMethod paymentMethodInstance = _factory.CreatePaymentMethod(paymentMethod);
            await paymentMethodInstance.Pay(paymentValue, customerId);


            return await Task.FromResult( new Order()
			{
				Value = paymentValue
			});
		}
	}
}
