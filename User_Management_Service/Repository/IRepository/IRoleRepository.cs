using System.Linq.Expressions;
using User_Management_Service.Context;

namespace User_Management_Service.Repository.IRepository
{
    public interface IRoleRepository
    {
        Task<List<Role>> GetList();
        Task<Role> GetRole(Expression<Func<Role, bool>> filter = null);
        Task Create(Role model);
        Task<Role> Update(Role model);
        Task Delete(int id);

    }
}
