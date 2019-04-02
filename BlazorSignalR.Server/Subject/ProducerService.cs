using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorSignalR.Server {

    public class ProducerService {

        private Dictionary<int, Subject> producers = new Dictionary<int, Subject>();
        private readonly IHubContext<MessageHub> _hubContext;

        public ProducerService(IHubContext<MessageHub> hub) {
            this._hubContext = hub;
            
        }


    }
}
