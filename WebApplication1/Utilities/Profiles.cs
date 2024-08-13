using AutoMapper;
using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Utilities
{
   
        public class AppProfile : Profile
        {
            public AppProfile()
            {
                CreateMap<Products, ProductDto>();
                CreateMap<CreateProductDto, Products>();
                CreateMap<UpdateProductDto, Products>();
                CreateMap<Users, GetUserProfileDto>();
                CreateMap<CreateUserProfileDto, Users>();
            }
        }
    
}
