namespace SSE_Redis_Recovery_Failure.App_Start
{
    using Funq;
    using ServiceStack;
    using ServiceStack.Host;
    using ServiceStack.Web;

    public class MyAppHost : AppHostBase
    {
        private readonly AppSetup Setup;

        // Tell the name and which assemblies contain web services.
        public MyAppHost()
            : base("My App")
        {
            Setup = new AppSetup();
        }

        public static void Start()
        {
            new MyAppHost().Init();
        }

        public override IServiceRunner<TRequest> CreateServiceRunner<TRequest>(ActionContext actionContext)
        {
            return AppSetup.CreateServiceRunner<TRequest>(this, actionContext);
        }

        public override void Configure(Container container)
        {
            Setup.Configure(this, container);
        }

        public override void OnAfterInit()
        {
            base.OnAfterInit();
            Setup.AfterConfigure();
        }
    }
}