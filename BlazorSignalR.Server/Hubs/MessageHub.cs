using BlazorSignalR.Shared;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorSignalR.Server {
    public class MessageHub:Hub {
        public  async Task SendMessage(Message message) {
            await this.Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
