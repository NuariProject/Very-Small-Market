using System.Linq.Expressions;
using User_Management_Service.Context;
using User_Management_Service.Models.DTO;

namespace User_Management_Service.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<List<UserDTO>> GetList();
        Task<UserDTO> GetUser(Expression<Func<User, bool>> filter = null);
        Task Create(UserDTO model);
        Task<UserDTO> Update(UserDTO model);
        Task Delete(int id);
    }
}
