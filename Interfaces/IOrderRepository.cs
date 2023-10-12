namespace ProvaPub.Interfaces
{
    public interface IOrderRepository
    {
        Task<int> CountOrdersInThisMonth(DateTime date, int customerId);
    }
}
