using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;
using WebApplication1.Repositories.Interfaces;

namespace WebApplication1.Repositories
{
    public class TenantsRepository : BaseRepository, ITenantsRepository
    {
        private readonly ApplicationDbContext _appContext;


        public TenantsRepository(ApplicationDbContext appContext, IHttpContextAccessor httpContextAccessor)
            : base(httpContextAccessor)
        {
            _appContext = appContext;

        }

        public async Task<PagedResponseDto<vw_api_tenants_me_users>> GetTenantsMyUsersAsync(int pageNumber, int pageSize)
        {
            var currentTenantID = GetTenantId();

            var totalRecords = await _appContext.vw_api_tenants_me_users.Where(c => c.TenantID == currentTenantID).CountAsync();

            var tenantList = await _appContext.vw_api_tenants_me_users.Where(c => c.TenantID == currentTenantID)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            return new PagedResponseDto<vw_api_tenants_me_users>(tenantList, pageNumber, pageSize, totalRecords);
        }

        public async Task<PagedResponseDto<vw_api_tenants_my_roles>> GetTenantsMyRolesAsync(int pageNumber, int pageSize)
        {
            var currentTenantID = GetTenantId();

            var totalRecords = await _appContext.vw_api_tenants_my_roles.Where(c => c.TenantID == currentTenantID).CountAsync();

            var roleList = await _appContext.vw_api_tenants_my_roles.Where(c => c.TenantID == currentTenantID)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            return new PagedResponseDto<vw_api_tenants_my_roles>(roleList, pageNumber, pageSize, totalRecords);
        }
    }
}
