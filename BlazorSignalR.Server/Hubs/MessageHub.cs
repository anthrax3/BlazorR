using BlazorSignalR.Shared;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorSignalR.Server {
    public class MessageHub:Hub {
        public  async Task<string> SendMessageAsync(Message message) {
            await this.Clients.All.SendAsync("ReceiveMessage", message);
            return DateTime.Now.ToString();
        }
        public async Task PingSelfAsync(Message message) {
            await this.Clients.Caller.SendAsync("ReceiveMessage", message);
        }
    }
}
