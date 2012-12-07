using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACAPackagesListener.API.http
{
    public class ServerResponse
    {
        private readonly List<String> messages_list; 
        public bool success { get; set; }
        public string[] messages { get; set; }
        public string objectData { get; set; }

        public ServerResponse()
        {
            messages_list = new List<string>();
        }

        public void addMessage(string newMessage)
        {
            messages_list.Add(newMessage);
            messages = messages_list.ToArray();
        }
    }
}
