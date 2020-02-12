using System;
using System.Collections.Generic;
using System.Diagnostics;
using Funq;
using ServiceStack;
using ServiceStack.Host;
using ServiceStack.Messaging;
using ServiceStack.Redis;
using ServiceStack.Web;
using WebApplication1.ServiceInterface;

namespace SSE_Redis_Recovery_Failure.App_Start
{
    public class AppSetup
    {
        public ServiceStackHost AppHost { get; set; }

        public Container Container { get; set; }

        public AppSetup()
        {
        }

        public void Configure(ServiceStackHost appHost, Container container)
        {
            AppHost = appHost;
            Container = container;

            Setup(Container);
            AfterContainerSetup();

            AppHost?.SetConfig(new HostConfig
            {
                AllowSessionIdsInHttpParams = true,
                DebugMode = false,
                EnableFeatures = Feature.All.Remove(Feature.Metadata)
            });

            Container.Register(c =>
                new SseService(c.Resolve<IServerEvents>())
            );
        }

        public void AfterConfigure()
        {
            var messageService = Container.Resolve<IMessageService>();
            messageService.Start();

            var eventService = Container.Resolve<IServerEvents>();
            eventService.Start();
        }

        private void Setup(Container container)
        {
            container.DefaultReuse = ReuseScope.Request;
        }

        protected virtual void AfterContainerSetup()
        {

            CpmServerSentEventsSetup = CpmServerSentEventsSetup.Initialize(this);

            AppHost.ServiceExceptionHandlers.Add(HandleException);

            AppHost.UncaughtExceptionHandlers.Add(HandleUnhandledException);

            AppHost.Config.UseSecureCookies = true;
            AppHost.Config.AllowNonHttpOnlyCookies = false;
        }

        public CpmServerSentEventsSetup CpmServerSentEventsSetup { get; set; }

        private object HandleException(IRequest httpReq, object request, Exception exception)
        {
            Debugger.Break();

            return null;
        }

        private void HandleUnhandledException(IRequest request, IResponse response, string operationName, Exception exception)
        {
            Debugger.Break();
        }

        public static IServiceRunner<TRequest> CreateServiceRunner<TRequest>(IAppHost appHost, ActionContext actionContext)
        {
            // Customized service runner for checking user rights.
            return new ButterflyServiceRunner<TRequest>(appHost, actionContext);
        }
    }

    public class CpmServerSentEventsSetup
    {
        private readonly AppSetup Setup;

        public CpmServerSentEventsSetup(AppSetup setup)
        {
            Setup = setup;
        }

        public static CpmServerSentEventsSetup Initialize(AppSetup appSetup)
        {
            var sse = new CpmServerSentEventsSetup(appSetup);
            sse.DoInitialize();
            return sse;
        }

        private Container Container => Setup.Container;

        private ServiceStackHost AppHost => Setup.AppHost;

        private void OnSubscribe(IEventSubscription eventSub)
        {
        }

        private void OnConnect(IEventSubscription eventSub, Dictionary<string, string> args)
        {
        }

        private void OnCreated(IEventSubscription eventSub, IRequest request)
        {

        }

        public void DoInitialize()
        {
            AppHost.Plugins.Add(new ServerEventsFeature
            {
                OnConnect = this.OnConnect,
                OnCreated = this.OnCreated,
                OnSubscribe = this.OnSubscribe,
                HeartbeatInterval = TimeSpan.FromSeconds(10),
                IdleTimeout = TimeSpan.FromMinutes(20),
                LimitToAuthenticatedUsers = true,
                NotifyChannelOfSubscriptions = false
            });

            Container.Register<IRedisClientsManager>(c =>
                new RedisManagerPool("localhost:6379")
            ).ReusedWithin(ReuseScope.Container);

            Container.Register<IServerEvents>(c =>
                new RedisServerEvents(c.Resolve<IRedisClientsManager>())
            ).ReusedWithin(ReuseScope.Container);
        }

    }

    public class ButterflyServiceRunner<T> : ServiceRunner<T>
    {
        public ButterflyServiceRunner(IAppHost appHost, ActionContext actionContext)
            : base(appHost, actionContext)
        {
        }

        public override object AfterEachRequest(IRequest requestContext, T request, object response, object service)
        {
            return base.AfterEachRequest(requestContext, request, response, service);
        }

        public override void OnBeforeExecute(IRequest requestContext, T request, object service)
        {

        }

        private TService Resolve<TService>()
        {
            return AppHost.GetContainer().Resolve<TService>();
        }
    }
}