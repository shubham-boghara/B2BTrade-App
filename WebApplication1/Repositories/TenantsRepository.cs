﻿using AutoMapper;
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

            var tenantList = await _appContext.vw_api_tenants_me_users.Where(c => c.TenantID == currentTenantID)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            return new PagedResponseDto<vw_api_tenants_me_users>(tenantList, pageNumber, pageSize, totalRecords);
        }

        public async Task<PagedResponseDto<vw_api_tenants_my_roles>> GetRolesAsync(int pageNumber, int pageSize)
        {
            var currentTenantID = GetTenantId();

            var totalRecords = await _appContext.vw_api_tenants_my_roles.Where(c => c.TenantID == currentTenantID).CountAsync();

            var roleList = await _appContext.vw_api_tenants_my_roles.Where(c => c.TenantID == currentTenantID)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            return new PagedResponseDto<vw_api_tenants_my_roles>(roleList, pageNumber, pageSize, totalRecords);
        }

        public async Task<vw_api_tenants_my_roles> GetRoleByIdAsync(int id)
        {
            var currentTenantID = GetTenantId();

            var roleByPk = await _appContext.vw_api_tenants_my_roles.SingleOrDefaultAsync(c => c.RoleID == id);

            return roleByPk;
        }

        public async Task<vw_api_tenants_my_roles> CreateRoleAsync(CreateRoleDto createRoleDto)
        {
            var currentTenantID = GetTenantId();
            var role = _mapper.Map<Roles>(createRoleDto);
            role.TenantID = currentTenantID;

            _appContext.Roles.Add(role);
            await _appContext.SaveChangesAsync();

            var roleByPk = await GetRoleByIdAsync(role.RoleID);

            return roleByPk;

        }
    }
}
