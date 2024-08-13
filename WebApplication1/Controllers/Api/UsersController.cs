using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers.Api
{
    [Route("api/v1/Users")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService) {

            _userService = userService;
            
        }

        [HttpGet]
        [Route("Me/Profile")]
        public async Task<IActionResult> GetMyProfile()
        {
            var user = await _userService.GetUserProfileAsync();

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        [Route("Me/Profile")]
        public async Task<IActionResult> CreateMyProfile(CreateUserProfileDto createUserProfileDto)
        {
            if (!ModelState.IsValid) { 
               return BadRequest(ModelState);
            }

            var user = await _userService.CreateUserProfileAsync(createUserProfileDto);

            return Ok(user);
        }

        

    }
}
