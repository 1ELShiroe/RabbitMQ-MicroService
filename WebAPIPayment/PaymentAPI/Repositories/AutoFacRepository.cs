using Autofac;
using PaymentAPI.Repositories.interfaces;

namespace PaymentAPI.Repositories
{
    public class AutoFacRepository : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PaymentRepository>().As<IPaymentRepository>().InstancePerLifetimeScope();
        }
    }
}