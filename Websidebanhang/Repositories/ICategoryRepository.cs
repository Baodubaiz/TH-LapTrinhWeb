using Websidebanhang.Models;

namespace Websidebanhang.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAllCategories();   
    }
}
