using AutoMapper;
using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Repositories.Interfaces;
using WebApplication1.Services.Interfaces;

namespace WebApplication1.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper, IUserRepository userRepository) {

              _mapper = mapper;
              _userRepository = userRepository;
        }

        public async Task<GetUserProfileDto> CreateUserProfileAsync(CreateUserProfileDto createUserProfileDto)
        {

            var getUser = await _userRepository.GetUserProfileAsync();

            if (getUser == null) {

                var user = _mapper.Map<Users>(createUserProfileDto);
                var createUser = await _userRepository.CreateUserProfileAsync(user);
                return _mapper.Map<GetUserProfileDto>(createUser);
            }
            else
            {
                _mapper.Map(createUserProfileDto, getUser);
                var updateUser = await _userRepository.CreateUserProfileAsync(getUser);
                return _mapper.Map<GetUserProfileDto>(updateUser);
            }

            return null;
        }

        public async Task<GetUserProfileDto> GetUserProfileAsync()
        {
            var user = await _userRepository.GetUserProfileAsync();
            return _mapper.Map<GetUserProfileDto>(user);
        }
    }
}
