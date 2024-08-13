﻿using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<Users> GetUserProfileAsync();

        Task<Users> CreateUserProfileAsync(Users users);

    }
}
