using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorSignalR.Server {
    public class Subject {
        
        private Task produceTask;
        private IClientProxy client;
        private IMessageProducer<string> producer;
        private readonly string Id;
        public Subject(string id,IClientProxy client,IMessageProducer<string>producer) {
            this.Id = id;
            this.client = client;
            this.producer = producer;

        }

        private async Task RunAsync() {
            while (true) {
                string newMessage = await producer.ProduceAsync();
                await client.SendAsync("ReceiveAsync", newMessage);
            }
        }
        
    }
}
