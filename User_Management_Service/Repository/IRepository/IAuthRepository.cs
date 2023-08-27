using System.Linq.Expressions;
using User_Management_Service.Context;
using User_Management_Service.Models.DTO;

namespace User_Management_Service.Repository.IRepository
{
    public interface IAuthRepository
    {
        Task<string> Authentication(AuthDTO model, UserDTO user);

    }
}
