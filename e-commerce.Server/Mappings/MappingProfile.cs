using AutoMapper;
using e_commerce.Server.DTOs;
using e_commerce.Server.Models;

namespace e_commerce.Server.Mappings
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();

         
        }
    }
}
