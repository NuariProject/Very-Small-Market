using Product_Management_Service.Context;
using System.Linq.Expressions;

namespace Product_Management_Service.Repository.IRepository
{
    public interface  ICategoryRepository
    {
        Task<List<Category>> GetList();
        Task<Category> GetCategory(Expression<Func<Category, bool>> filter = null);
        Task Create(Category model);
        Task<Category> Update(Category model);
        Task Delete(int id);

    }
}
