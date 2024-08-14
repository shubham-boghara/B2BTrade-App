using System;
using System.Security.Claims;
using WebApplication1.Data;

namespace WebApplication1.Repositories
{
    public class BaseRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BaseRepository(IHttpContextAccessor httpContextAccessor)
        {

            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId()
        {
            return _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }

        public int GetTenantId()
        {
            return Convert.ToInt32(_httpContextAccessor?.HttpContext?.User?.FindFirst("TenantID")?.Value);
        }
    }
}
