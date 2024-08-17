using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.DTOs;
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
        [Route("my/users")]
        public async Task<IActionResult> GetUsers([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var users = await _tenantsRepository.GetUsersAsync(pageNumber, pageSize);

            return Ok(users);
        }

        [HttpGet]
        [Route("my/roles")]
        public async Task<IActionResult> GetRoles([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var roles = await _tenantsRepository.GetRolesAsync(pageNumber, pageSize);

            return Ok(roles);
        }

        [HttpGet]
        [Route("my/roles/{id}")]
        public async Task<IActionResult> GetRole(int id)
        {

            var role = await _tenantsRepository.GetRoleByIdAsync(id);

            if (role == null) {
                return NotFound();
            }

            return Ok(role);
        }

        [HttpPost]
        [Route("my/roles")]
        public async Task<IActionResult> CreateRole(CreateRoleDto createRoleDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdRole = await _tenantsRepository.CreateRoleAsync(createRoleDto);

            return Ok(createdRole);
        }

        [HttpPut]
        [Route("my/roles/{id}")]
        public async Task<IActionResult> UpdateRole(int id, UpdateRoleDto updateRoleDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var UpdatedRole = await _tenantsRepository.UpdateRoleAsync(id, updateRoleDto);

            return Ok(UpdatedRole);
        }

        [HttpDelete]
        [Route("my/roles/{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {

            var result = await _tenantsRepository.DeleteRoleAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        [Route("chat/users")]
        public async Task<IActionResult> ChatUsers([FromBody] ChatUserDto? chatUserDto)
        {
            var users = await _tenantsRepository.ChatUsersAsync(chatUserDto);

            return Ok(users);
        }
    }
}
