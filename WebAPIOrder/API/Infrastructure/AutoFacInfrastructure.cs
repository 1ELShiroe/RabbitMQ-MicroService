using API.Infrastructure.Interface;
using Autofac;

namespace API.Infrastructure.Context
{
    public class AutoFacInfrastructure : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationContext>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<Consumer>().As<IConsumer>().As<IHostedService>().InstancePerLifetimeScope();
            builder.RegisterType<Publisher>().As<IPublisher>().InstancePerLifetimeScope();
        }
    }
}