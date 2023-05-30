using PaymentAPI.infrastructure.Interfaces;
using Autofac;
using PaymentAPI.infrastructure.MessageQueue;

namespace PaymentAPI.infrastructure
{
    public class AutoFacInfrastructure : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Consumer>().As<IConsumer>().InstancePerLifetimeScope();
            builder.RegisterType<Publisher>().As<IPublisher>().InstancePerLifetimeScope();
        }
    }
}