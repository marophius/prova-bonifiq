namespace ProvaPub.Models
{
    public class ModelList<T> where T : class
    {
        public List<T> Items { get; set; }
        public int TotalCount { get; set; } = 10;
        public bool HasNext { get; set; } = false;
    }
}
