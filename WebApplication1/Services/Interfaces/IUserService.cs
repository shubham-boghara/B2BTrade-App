using WebApplication1.DTOs;

namespace WebApplication1.Services.Interfaces
{
    public interface IUserService
    {
        Task<GetUserProfileDto> GetUserProfileAsync();

        Task<GetUserProfileDto> CreateUserProfileAsync(CreateUserProfileDto createUserProfileDto);
    }
}
