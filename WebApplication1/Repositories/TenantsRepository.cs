using AutoMapper;
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
        private readonly IMapper _mapper;

        public TenantsRepository(ApplicationDbContext appContext, IHttpContextAccessor httpContextAccessor, IMapper mapper)
            : base(httpContextAccessor)
        {
            _appContext = appContext;
            _mapper = mapper;
        }

        public async Task<PagedResponseDto<vw_api_tenants_me_users>> GetUsersAsync(int pageNumber, int pageSize)
        {
            var currentTenantID = GetTenantId();

            var totalRecords = await _appContext.vw_api_tenants_me_users.Where(c => c.TenantID == currentTenantID).CountAsync();

            var users = await _appContext.vw_api_tenants_me_users.Where(c => c.TenantID == currentTenantID)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            return new PagedResponseDto<vw_api_tenants_me_users>(users, pageNumber, pageSize, totalRecords);
        }

        public async Task<PagedResponseDto<vw_api_tenants_my_roles>> GetRolesAsync(int pageNumber, int pageSize)
        {
            var currentTenantID = GetTenantId();

            var totalRecords = await _appContext.vw_api_tenants_my_roles.Where(c => c.TenantID == currentTenantID).CountAsync();

            var roles = await _appContext.vw_api_tenants_my_roles.Where(c => c.TenantID == currentTenantID)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            return new PagedResponseDto<vw_api_tenants_my_roles>(roles, pageNumber, pageSize, totalRecords);
        }

        public async Task<vw_api_tenants_my_roles> GetRoleByIdAsync(int id)
        {
            var currentTenantID = GetTenantId();

            var roleById = await _appContext.vw_api_tenants_my_roles.SingleOrDefaultAsync(c => c.RoleID == id);

            return roleById;
        }

        public async Task<vw_api_tenants_my_roles> CreateRoleAsync(CreateRoleDto createRoleDto)
        {
            var currentTenantID = GetTenantId();
            var role = _mapper.Map<Roles>(createRoleDto);
            role.TenantID = currentTenantID;

            _appContext.Roles.Add(role);
            await _appContext.SaveChangesAsync();

            var roleById = await GetRoleByIdAsync(role.RoleID);

            return roleById;

        }

        public async Task<vw_api_tenants_my_roles> UpdateRoleAsync(int id, UpdateRoleDto updateRoleDto)
        {
            var currentTenantID = GetTenantId();

            var role = await RolesAsync(id);

            if(role == null)
            {
                return null;
            }

            _mapper.Map(updateRoleDto, role);
            await _appContext.SaveChangesAsync();

            var roleById = await GetRoleByIdAsync(role.RoleID);

            return roleById;
        }

        public async Task<bool> DeleteRoleAsync(int id)
        {
            var role = await RolesAsync(id);

            if(role == null)
            {
                return false;
            }

            _appContext.Roles.Remove(role);
            await _appContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<vw_api_tenants_chat_users>> ChatUsersAsync(ChatUserDto chatUserDto)
        {
            var currentTenantID = GetTenantId();

            IQueryable<vw_api_tenants_chat_users> users = _appContext.vw_api_tenants_chat_users.Where(c => c.TenantID == currentTenantID);

            if (chatUserDto != null && chatUserDto.Ids.Any())
            {
                var filteredUsers = await users.Where(c => chatUserDto.Ids.Contains(c.AspUserID)).ToListAsync();
                return filteredUsers;
            }
            else
            {
                var getUsers = await users.ToListAsync();
                return getUsers;
            }
        }

        public async Task<Roles> RolesAsync(int id) {

            return await _appContext.Roles.FindAsync(id);
        }
    }
}
