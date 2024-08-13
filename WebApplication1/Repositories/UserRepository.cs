using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Repositories.Interfaces;

namespace WebApplication1.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly ApplicationDbContext _appContext;
        

        public UserRepository(ApplicationDbContext appContext, IHttpContextAccessor httpContextAccessor)
            :base(httpContextAccessor)
        {
            _appContext = appContext;
          
        }

        public async Task<Users> CreateUserProfileAsync(Users users)
        {
            if (users.UserID > 0) {
                users.UpdatedAt = DateTime.Now;
                await _appContext.SaveChangesAsync();
                return users;
            }
            else
            {
                users.CreatedAt = DateTime.Now;
                users.AspUserID = GetUserId();
                _appContext.Add(users);
                await _appContext.SaveChangesAsync();
                return users;
            }
        }

        public async Task<Users> GetUserProfileAsync()
        {
            var userId = GetUserId();

            var user = await _appContext.Users.SingleOrDefaultAsync(c => c.AspUserID == userId);

            return user;
        }

       
    }
}
