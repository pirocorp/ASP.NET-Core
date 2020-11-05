namespace _01._Chat.Hubs
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR;

    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            // RPC
            await this.Clients.All.SendAsync(method: "ReceiveMessage", user, message);
        }
    }
}
