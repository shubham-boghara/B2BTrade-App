using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace WebApplication1.Repositories
{
    public abstract class BaseRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        protected BaseRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public string GetUserId()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                throw new InvalidOperationException("User ID is not available in the current context.");
            }

            return userId;
        }

        public int GetTenantId()
        {
            var tenantIdValue = _httpContextAccessor.HttpContext?.User?.FindFirst("TenantID")?.Value;

            if (string.IsNullOrEmpty(tenantIdValue) || !int.TryParse(tenantIdValue, out int tenantId))
            {
                throw new InvalidOperationException("Tenant ID is not available or invalid in the current context.");
            }

            return tenantId;
        }
    }
}
