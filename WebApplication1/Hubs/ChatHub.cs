using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Security.Claims;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        /*public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }*/

        private static readonly ConcurrentDictionary<string, string> Users = new ConcurrentDictionary<string, string>();

        public override async Task OnConnectedAsync()
        {
            var tenantId = Context.User?.FindFirst("TenantID")?.Value;
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrEmpty(tenantId) && !string.IsNullOrEmpty(userId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, tenantId);
                Users[Context.ConnectionId] = userId;

                await SendUserListUpdate(tenantId);
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
                await SendUserListUpdate(tenantId);
            }

            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string user, string message)
        {
            var tenantId = Context.User?.FindFirst("TenantID")?.Value;

            var fromUser = Context.User?.Identity.Name;

            if (!string.IsNullOrEmpty(tenantId))
            {


                await Clients.Group(tenantId).SendAsync("ReceiveMessage", fromUser, user, message);
            }
        }

        private Task SendUserListUpdate(string tenantId)
        {
            var usersInTenant = Users.Values.Distinct().ToList();
            return Clients.Group(tenantId).SendAsync("UserListUpdated", usersInTenant);
        }
    }
}