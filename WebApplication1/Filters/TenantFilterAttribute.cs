using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Middlewares;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Filters
{
    public class TenantFilterAttribute : IAsyncActionFilter
    {
        private readonly ApplicationDbContext _context;

        public TenantFilterAttribute(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var userId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var tenantIdClaim = context.HttpContext.User.FindFirst("TenantId")?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                context.Result = new ForbidResult();
                return;
            }

            if (tenantIdClaim == null || !int.TryParse(tenantIdClaim, out var tenantId))
            {
                context.Result = new BadRequestObjectResult("Tenant ID is missing or invalid in claims.");
                return;
            }

            var userTenantUser = await _context.TenantUsers
                .SingleOrDefaultAsync(c => c.TenantID == tenantId && c.AspUserID == userId);

            if (userTenantUser == null)
            {
                context.Result = new ForbidResult();
                return;
            }

            context.HttpContext.Items["TenantId"] = tenantId.ToString();
            await next();
        }
    }

    /*public class TenantFilterAttribute : IAsyncActionFilter
    {
        private readonly ApplicationDbContext _context;

        public TenantFilterAttribute(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var userId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var tenantIdClaim = context.HttpContext.User.FindFirst("TenantId")?.Value;

            if (tenantIdClaim != null && int.TryParse(tenantIdClaim, out var tenantId))
            {
               // var getTenant = await _context.Tenants.FindAsync(tenantId);

                var userTenantUser = await _context.TenantUsers
                    .SingleOrDefaultAsync(c => c.TenantID == tenantId && c.AspUserID == userId);

                if (userTenantUser == null)
                {
                    context.Result = new ForbidResult();
                    return;
                }

                // Store the tenant ID in HttpContext
                context.HttpContext.Items["TenantId"] = tenantId.ToString();
                //context.HttpContext.Items["TenantName"] = getTenant?.TenantName;
                //context.HttpContext.Items["AspUserID"] = userId;
                //context.HttpContext.Items["AccessType"] = "all-data";
            }
            else
            {
                context.Result = new BadRequestObjectResult("Tenant ID is missing in claims.");
                return;
            }

            await next();
        }

        *//* public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
         {
             if (context.HttpContext.Request.Headers.TryGetValue("X-Tenant-ID", out var tenantId))
             {
                 var userId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                 var convertTenantID = Convert.ToInt32(tenantId);

                 var userTenantUser = await _context.TenantUsers
                     .SingleOrDefaultAsync(c => c.TenantID == convertTenantID && c.AspUserID == userId);

                 if (userTenantUser == null)
                 {
                     context.Result = new ForbidResult();
                     return;
                 }

                 // Store the tenant ID in HttpContext
                 context.HttpContext.Items["TenantId"] = tenantId.ToString();
                 context.HttpContext.Items["AspUserID"] = userId;
                 context.HttpContext.Items["AccessType"] = "all-data";
             }
             else
             {
                 context.Result = new BadRequestObjectResult("Tenant ID is missing.");
                 return;
             }

             await next();
         }*//*
    }*/
}