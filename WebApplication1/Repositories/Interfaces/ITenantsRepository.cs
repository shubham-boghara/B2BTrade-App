using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Repositories.Interfaces
{
    public interface ITenantsRepository
    {
        //{verb}{action}{async}
        Task<PagedResponseDto<vw_api_tenants_me_users>> GetUsersAsync(int pageNumber, int pageSize);

        Task<PagedResponseDto<vw_api_tenants_my_roles>> GetRolesAsync(int pageNumber, int pageSize);

        Task<vw_api_tenants_my_roles> GetRoleByIdAsync(int id);

        Task<vw_api_tenants_my_roles> CreateRoleAsync(CreateRoleDto createRoleDto);

        Task<vw_api_tenants_my_roles> UpdateRoleAsync(int id, UpdateRoleDto updateRoleDto);

        Task<Roles> RolesAsync(int id);

        Task<bool> DeleteRoleAsync(int id);
    }
}
