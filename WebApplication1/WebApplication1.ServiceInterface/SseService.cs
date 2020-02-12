using ServiceStack;
using WebApplication1.ServiceModel;

namespace WebApplication1.ServiceInterface
{
    public class SseService : Service
    {
        public IServerEvents ServerEvents { get; set; }

        public SseService()
        {
            
        }

        public SseService(IServerEvents serverEvents)
        {
            ServerEvents = serverEvents;
        }

        public string Post(SubscribeServerSentEventRequest request)
        {
            ServerEvents.SubscribeToChannels(null,new[] { request.Channel });

            return "SSE subscribed";
        }

        public string Post(ServerEventMessageRequest request)
        {
            ServerEvents.NotifyChannel(request.Channel, request.Message);

            return "SSE Message sent";
        }
    }
}