using ServiceStack;

namespace WebApplication1.ServiceModel
{
    [Route("/sse/message/{Message}", "POST")]
    public class ServerEventMessageRequest : IReturn<string>
    {
        public string Message { get; set; }
    }
}