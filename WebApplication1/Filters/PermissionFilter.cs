using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication1.Data;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Filters
{
    public class PermissionFilter : Attribute, IAsyncActionFilter
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        //private readonly string _validatePath;

        public PermissionFilter(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            //_validatePath = validatePath;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var userId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var tenantId = Convert.ToInt32(_httpContextAccessor.HttpContext?.Items["TenantId"]?.ToString());
            var currentUrlPath = context.HttpContext.Request.Path.Value?.Trim('/');

            var userTenantUser = await _context.TenantUsers.SingleOrDefaultAsync(c => c.TenantID == tenantId && c.AspUserID == userId);

            if (userTenantUser == null)
            {
                context.Result = new ForbidResult();
                return;
            }

            var query = _context.Vw_Permissions.Where(
                c => c.RoleID == userTenantUser.RoleID && c.FormUrl == currentUrlPath
            );

            var httpMethod = context.HttpContext.Request.Method;
            bool permissionGranted = false;
            Vw_Permissions? permissions = null;

            switch (httpMethod)
            {
                case "GET":
                    permissions = await query.SingleOrDefaultAsync(c => c.CanView == true);
                    permissionGranted = permissions != null;
                    break;
                case "POST":
                    permissions = await query.SingleOrDefaultAsync(c => c.CanAdd == true);
                    permissionGranted = permissions != null;
                    break;
                case "PUT":
                case "PATCH":
                    permissions = await query.SingleOrDefaultAsync(c => c.CanEdit == true);
                    permissionGranted = permissions != null;
                    break;
                case "DELETE":
                    permissions = await query.SingleOrDefaultAsync(c => c.CanDelete == true);
                    permissionGranted = permissions != null;
                    break;
            }

            if (!permissionGranted)
            {
                context.Result = new ForbidResult();
                return;
            }

            context.HttpContext.Items["AccessType"] = permissions?.AccessType.ToString();
            context.HttpContext.Items["AspUserID"] = userId;

            await next();
        }

    }
}
