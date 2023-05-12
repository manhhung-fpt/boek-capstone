using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;

namespace Boek.Service.Hubs
{
    public class NotificationHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            var claim = Context.User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier));
            if (claim != null)
                Groups.AddToGroupAsync(Context.ConnectionId, claim.Value);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var claim = Context.User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier));
            if (claim != null)
                Groups.RemoveFromGroupAsync(Context.ConnectionId, claim.Value);
            return base.OnDisconnectedAsync(exception);
        }
    }
}