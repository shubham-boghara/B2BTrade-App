using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication1.Filters;
using WebApplication1.Repositories;
using WebApplication1.Repositories.Interfaces;

namespace WebApplication1.Controllers.Api
{
    [Route("api/v1/chat")]
    [ApiController]
    [Authorize]
    [ServiceFilter(typeof(TenantFilterAttribute))]
    public class ChatController : ControllerBase
    {
        private readonly IChatRepository _chatRepository;

        public ChatController(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        [HttpGet("messages/{userId}")]
        public async Task<IActionResult> GetMessages(string userId)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var messages = await _chatRepository.GetMessagesAsync(currentUserId, userId);
            return Ok(messages);
        }
    }
}
