using ProvaPub.Interfaces;

namespace ProvaPub.Models.PaymentMethods
{
    public class PayPalPayment : IPaymentMethod
    {
        public Task Pay(decimal paymentValue, int customerId)
        {
            return Task.CompletedTask;
        }
    }
}
