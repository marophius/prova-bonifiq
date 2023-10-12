namespace ProvaPub.Interfaces
{
    public interface IPaymentMethodFactory
    {
        IPaymentMethod CreatePaymentMethod(string paymentMethod);
    }
}
