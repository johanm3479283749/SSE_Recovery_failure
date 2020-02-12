using ServiceStack;

using WebApplication1.ServiceInterface;

namespace WebApplication1
{
    public class BasicAuthFeature : IPlugin
    {
        public string HtmlRedirect { get; set; }

        public void Register(IAppHost appHost)
        {
            appHost.RegisterService<SseService>();

            appHost.LoadPlugin(new SessionFeature());
        }
    }
}