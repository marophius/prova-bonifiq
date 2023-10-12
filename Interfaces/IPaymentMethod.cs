namespace ProvaPub.Interfaces
{
    public interface IPaymentMethod
    {
        Task Pay(decimal paymentValue, int customerId);
    }
}
