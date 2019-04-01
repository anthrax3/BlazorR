using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorSignalR.Shared {
    public class Message {
        public string Id { get; set; }
        public string Payload { get; set; }
    }
}
