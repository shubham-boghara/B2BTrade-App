using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Filters;
using WebApplication1.Repositories;
using WebApplication1.Repositories.Interfaces;

namespace WebApplication1.Controllers.Api
{
    /*[Route("api/[controller]")]*/
    [Route("api/v1/tenants")]
    [ApiController]
    [Authorize]
    [ServiceFilter(typeof(TenantFilterAttribute))]
    public class TenantsController : ControllerBase
    {

        private readonly ITenantsRepository _tenantsRepository;
        public TenantsController(ITenantsRepository tenantsRepository)
        {
            _tenantsRepository = tenantsRepository;
        }

        [HttpGet]
        [Route("My/Users")]
        public async Task<IActionResult> MyUsers([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var users = await _tenantsRepository.GetTenantsMyUsersAsync(pageNumber, pageSize);

            return Ok(users);
        }

        [HttpGet]
        [Route("My/Roles")]
        public async Task<IActionResult> Roles([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var roles = await _tenantsRepository.GetTenantsMyRolesAsync(pageNumber, pageSize);

            return Ok(roles);
        }


    }
}
