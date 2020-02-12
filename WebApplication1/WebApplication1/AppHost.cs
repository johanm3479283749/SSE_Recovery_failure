using System;
using System.Collections.Generic;
using System.Diagnostics;
using Funq;
using ServiceStack;
using ServiceStack.Redis;
using ServiceStack.Web;
using WebApplication1.ServiceInterface;

namespace WebApplication1
{
    public class AppHost : AppHostBase
    {
        public AppHost()
            : base("WebApplication1", typeof(SseService).Assembly) { }

        private void OnSubscribe(IEventSubscription eventSub)
        {

        }

        private void OnConnect(IEventSubscription eventSub, Dictionary<string, string> args)
        {

        }

        private void OnCreated(IEventSubscription eventSub, IRequest request)
        {
        }

        private void OnPublish(IEventSubscription arg1, IResponse arg2, string arg3)
        {
            Debug.WriteLine(arg3);
        }

        public override void Configure(Container container)
        {
            Plugins.Add(new ServerEventsFeature
            {
                OnConnect = this.OnConnect,
                OnCreated = this.OnCreated,
                OnSubscribe = this.OnSubscribe,
                OnPublish = this.OnPublish,
                HeartbeatInterval = TimeSpan.FromSeconds(10),
                IdleTimeout = TimeSpan.FromMinutes(20),
                LimitToAuthenticatedUsers = false,
                NotifyChannelOfSubscriptions = false
            });
            Plugins.Add(new BasicAuthFeature { HtmlRedirect = "~/login" });

            SetConfig(new HostConfig
            {
                AllowSessionIdsInHttpParams = true,
                DebugMode = false,
                EnableFeatures = Feature.All.Remove(Feature.Metadata)
            });

            Container.Register(c =>
                new SseService(c.Resolve<IServerEvents>())
            );

            Container.Register<IRedisClientsManager>(c =>
                new RedisManagerPool("localhost:6379")
            );

            Container.Register<IServerEvents>(c =>
                new RedisServerEvents(c.Resolve<IRedisClientsManager>())
            );

            Container.Resolve<IServerEvents>().Start();

            Container.DefaultReuse = ReuseScope.Request;

            ServiceExceptionHandlers.Add(HandleException);

            UncaughtExceptionHandlers.Add(HandleUnhandledException);
        }

        private object HandleException(IRequest httpReq, object request, Exception exception)
        {
            Debug.WriteLine(exception.Message);

            return null;
        }

        private void HandleUnhandledException(IRequest request, IResponse response, string operationName, Exception exception)
        {
            Debug.WriteLine(exception.Message);

            Debugger.Break();
        }
    }

}