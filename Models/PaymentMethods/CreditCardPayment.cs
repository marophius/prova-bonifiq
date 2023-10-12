using ProvaPub.Interfaces;

namespace ProvaPub.Models.PaymentMethods
{
    public class CreditCardPayment : IPaymentMethod
    {
        public Task Pay(decimal paymentValue, int customerId)
        {
            return Task.CompletedTask;
        }
    }
}
