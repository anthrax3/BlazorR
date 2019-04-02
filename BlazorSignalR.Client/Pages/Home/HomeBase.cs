using Blazor.Extensions;
using BlazorSignalR.Shared;
using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorSignalR.Client {
    public class HomeBase:BlazorComponent {
        protected Message message = new Message();
        
        private HubConnection con { get; set; }

        protected string GetBtnName => this.IsOpen ? "Close" : "Open";
        private bool isOpen;
        protected bool IsOpen {
            get {
                return this.isOpen;
            }
            set {
                this.isOpen = value;

            }
        }
        protected override async Task OnInitAsync() {
            try {
                this.con = new HubConnectionBuilder().WithUrl("/message", builder => {
                    builder.LogLevel = SignalRLogLevel.Trace;
                    builder.Transport = HttpTransportType.WebSockets;
                }).Build();
            } catch (Exception) {

                throw;
            }

            con.On<Message>("ReceiveMessage", this.OnMessageHandle);
        }
        protected async Task OnMessageHandle(Message msg) {
            Console.WriteLine($"Received from:{msg.Id}+,payload:{msg.Payload}");
        }
        protected async Task SendMessageAsync() {
            string rez = await con.InvokeAsync<string>("SendMessageAsync", this.message);
            Console.WriteLine("Result of sending :" + rez);
        }
        protected async Task PingSelfAsync() {
            string rez=await con.InvokeAsync<string>("PingSelfAsync", this.message);
            Console.WriteLine("Result of sending:" + rez);
        }
        protected async Task ManageConnectionAsync() {
            if (this.IsOpen) {
                await CloseConnection();
                this.IsOpen = true;
            } else {
                await OpenConnection();
                this.IsOpen = false;
            }
               
            
        }
        protected async Task OpenConnection() {
            await this.con.StartAsync();
        }
        protected async Task CloseConnection() {
             await this.con.StopAsync();
        }

    }
}
