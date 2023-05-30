using API.Infrastructure.Interface;
using API.Infrastructure.Context;
using Moq;

namespace Tests.Infraestructure.Context
{
    public class AutoFacApplicationContextTests : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var publisherMock = new Mock<IPublisher>();

            builder.RegisterType<ApplicationContextTests>().As<ApplicationContext>().InstancePerLifetimeScope();
            builder.RegisterInstance(publisherMock.Object).As<IPublisher>();
        }
    }
}