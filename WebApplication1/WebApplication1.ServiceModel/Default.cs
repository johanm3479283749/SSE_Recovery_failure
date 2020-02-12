using ServiceStack;

namespace WebApplication1.ServiceModel
{
    [FallbackRoute("/")]
    public class Default : IReturn<DefaultResponse>
    {
    }
}