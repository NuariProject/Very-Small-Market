using AutoMapper;
using Product_Management_Service.Context;
using Product_Management_Service.Models.DTO;

namespace Product_Management_Service
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Category, CategoryCreatedDTO>().ReverseMap();
            CreateMap<Category, CategoryUpdatedDTO>().ReverseMap();

            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<ProductDTO, ProductCreatedDTO>().ReverseMap()
                .ForMember(dest => dest.CategoryList, opt => opt.MapFrom(src => src.CategoryList))
                .ReverseMap();
            CreateMap<ProductDTO, ProductUpdatedDTO>().ReverseMap()
                .ForMember(dest => dest.CategoryList, opt => opt.MapFrom(src => src.CategoryList))
                .ReverseMap();
        }
    }
}
