using NUnit.Framework;
using ServiceStack;
using ServiceStack.Testing;
using WebApplication1.ServiceInterface;
using WebApplication1.ServiceModel;

namespace WebApplication1.Tests
{
    public class UnitTest
    {
        private readonly ServiceStackHost appHost;

        public UnitTest()
        {
            appHost = new BasicAppHost().Init();
            appHost.Container.AddTransient<MyServices>();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown() => appHost.Dispose();
    }
}
