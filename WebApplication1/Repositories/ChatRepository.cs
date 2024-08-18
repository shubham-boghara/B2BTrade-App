using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Repositories.Interfaces;

namespace WebApplication1.Repositories
{
    public class ChatRepository : BaseRepository, IChatRepository
    {
        private readonly TenantDbContext _context;

        public ChatRepository(TenantDbContext context, IHttpContextAccessor httpContextAccessor):
            base(httpContextAccessor)
        {
            _context = context;
        }

        public async Task<List<ChatMessages>> GetMessagesAsync(string userId1, string userId2)
        {
            return await _context.ChatMessages
            .Where(m => (m.SenderId == userId1 && m.ReceiverId == userId2) ||
                        (m.SenderId == userId2 && m.ReceiverId == userId1))
            .OrderBy(m => m.CreatedAt)
            .ToListAsync();
        }

        public async Task SaveMessageAsync(ChatMessages message)
        {
            _context.Add(message);
            await _context.SaveChangesAsync();
        }
    }
}
