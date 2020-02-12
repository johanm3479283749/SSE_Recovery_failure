using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using ServiceStack;
using WebApplication1.ServiceModel;

namespace WebApplication1.ServiceInterface
{
    public class SseService : Service
    {
        public IServerEvents ServerEvents { get; set; }

        public SseService(IServerEvents serverEvents)
        {
            ServerEvents = serverEvents;
        }

        public void Post(SubscribeServerSentEventRequest request)
        {
            ServerEvents.SubscribeToChannels(null, new[] {request.Channel});
        }

        public void Post(ServerEventMessageRequest request)
        {
            ServerEvents.NotifyAll(request.Message);
        }
    }
}