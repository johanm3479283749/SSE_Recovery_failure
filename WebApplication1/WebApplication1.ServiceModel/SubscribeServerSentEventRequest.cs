using ServiceStack;

namespace WebApplication1.ServiceModel
{
    [Route("/sse/subscribe/{Channel}", "POST")]
    public class SubscribeServerSentEventRequest: IReturn<string>
    {
        public string Channel { get; set; }
    }
}