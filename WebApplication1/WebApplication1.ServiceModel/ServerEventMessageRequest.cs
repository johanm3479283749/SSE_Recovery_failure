using ServiceStack;

namespace WebApplication1.ServiceModel
{
    [Route("/sse/message/{Channel}/{Message}", "POST")]
    public class ServerEventMessageRequest : IReturn<string>
    {
        public string Channel { get; set; }

        public string Message { get; set; }
    }
}