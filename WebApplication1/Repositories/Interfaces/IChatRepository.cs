using WebApplication1.Models;

namespace WebApplication1.Repositories.Interfaces
{
    public interface IChatRepository
    {
        Task SaveMessageAsync(ChatMessages message);

        Task<List<ChatMessages>> GetMessagesAsync(string userId1, string userId2);
    }
}
