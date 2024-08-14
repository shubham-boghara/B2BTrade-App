using WebApplication1.DTOs;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Repositories.Interfaces
{
    public interface ITenantsRepository
    {
        //{verb}{ctr}{action}{async}
         Task<PagedResponseDto<vw_api_tenants_me_users>> GetTenantsMyUsersAsync(int pageNumber, int pageSize);

        Task<PagedResponseDto<vw_api_tenants_my_roles>> GetTenantsMyRolesAsync(int pageNumber, int pageSize);
    }
}
