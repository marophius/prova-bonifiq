using ProvaPub.Interfaces;
using ProvaPub.Models.PaymentMethods;

namespace ProvaPub.Utils
{
    public class PaymentMethodFactory : IPaymentMethodFactory
    {
        public IPaymentMethod CreatePaymentMethod(string paymentMethod)
        {
            return paymentMethod switch
            {
                "pix" => new PixPayment(),
                "creditcard" => new CreditCardPayment(),
                "paypal" => new PayPalPayment(),
                _ => throw new InvalidOperationException(nameof(PaymentMethodFactory))
            };
        }
    }
}
