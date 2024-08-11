using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.ViewModels;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class UserDbContext : ApplicationDbContext
    {
        public UserDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


    }
}
