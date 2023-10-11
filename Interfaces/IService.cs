using ProvaPub.Models;

namespace ProvaPub.Interfaces
{
    public interface IService<T> where T : class
    {
        Task<ModelList<T>> GetList(int page);
    }
}
