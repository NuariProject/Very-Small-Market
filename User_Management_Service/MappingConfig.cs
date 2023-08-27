using AutoMapper;
using User_Management_Service.Context;
using User_Management_Service.Models.DTO;

namespace User_Management_Service
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Role, RoleDTO>().ReverseMap();
            CreateMap<Role, RoleCreateDTO>().ReverseMap();
            CreateMap<Role, RoleUpdateDTO>().ReverseMap();

            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<UserDTO, UserCreateDTO>().ReverseMap();
            CreateMap<UserDTO, UserUpdateDTO>().ReverseMap();
        }
    }
}
