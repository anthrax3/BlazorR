using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorSignalR.Server {
    abstract class DelayedProducer<T> : IProducer<T> {
        private IMessage<T> message;
        private T payload;
        private readonly int Interval;
        public DelayedProducer(int Interval,T msg) {
            this.Interval = Interval;
            this.payload = msg;
        }
        public async Task<T> ProduceAsync() {
            await Task.Delay(this.Interval);
            var m=await message.ProduceAsync(payload);
            return m;
        }
    }
}
