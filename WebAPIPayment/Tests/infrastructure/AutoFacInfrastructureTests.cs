using Moq;
using Autofac;
using PaymentAPI.infrastructure.Interfaces;

namespace Tests.infrastructure
{
    public class AutoFacInfrastructureTests : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var publisherMock = new Mock<IPublisher>();
            builder.RegisterInstance(publisherMock.Object).As<IPublisher>();
        }
    }
}