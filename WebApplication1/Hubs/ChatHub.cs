using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Security.Claims;
using WebApplication1.Filters;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.Repositories.Interfaces;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        /*public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }*/
        private readonly IChatRepository _chatRepository;


        private static readonly ConcurrentDictionary<string, string> Users = new ConcurrentDictionary<string, string>();

        public ChatHub(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public override async Task OnConnectedAsync()
        {
            var tenantId = Context.User?.FindFirst("TenantID")?.Value;
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrEmpty(tenantId) && !string.IsNullOrEmpty(userId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, tenantId);
                Users[Context.ConnectionId] = userId;

                await SendUserListUpdate();
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var tenantId = Context.User?.FindFirst("TenantID")?.Value;
            Users.TryRemove(Context.ConnectionId, out _);

            if (!string.IsNullOrEmpty(tenantId))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, tenantId);
                await SendUserListUpdate();
            }

            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string receiverId, string message)
        {
            var tenantId = Context.User?.FindFirst("TenantID")?.Value;

            var senderId = Context.UserIdentifier;
            var chatMessage = new ChatMessages
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Message = message,
                CreatedAt = DateTime.Now
            };

            if (!string.IsNullOrEmpty(tenantId))
            {
                await _chatRepository.SaveMessageAsync(chatMessage);

                await Clients.Group(tenantId).SendAsync("ReceiveMessage", senderId, receiverId, message);
            }
        }

        private Task SendUserListUpdate()
        {
            var tenantId = Context.User?.FindFirst("TenantID")?.Value;

            var usersInTenant = Users.Values.Distinct().ToList();
            return Clients.Group(tenantId).SendAsync("UserListUpdated", usersInTenant);
        }

        public async Task LoadMessages(string userId)
        {
            var tenantId = Context.User?.FindFirst("TenantID")?.Value;
            var currentUserId = Context.UserIdentifier;
            var messages = await _chatRepository.GetMessagesAsync(currentUserId, userId);
            await Clients.Group(tenantId).SendAsync("LoadMessages", messages);
        }
    }
}