using AutoMapper;
using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;

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

                CreateMap<CreateRoleDto, Roles>();
                //CreateMap<Roles, vw_api_tenants_my_roles>();
                CreateMap<UpdateRoleDto, Roles>();  

            }
        }
    
}
