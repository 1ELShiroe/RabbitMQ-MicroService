using Autofac;
using PaymentAPI.Services.Interface;

namespace PaymentAPI.Services
{
    public class AutoFacService : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PaymentCARD>().As<IPaymentCARD>().InstancePerLifetimeScope();
            builder.RegisterType<PaymentPIX>().As<IPaymentPIX>().InstancePerLifetimeScope();
            builder.RegisterType<PaymentTicket>().As<IPaymentTicket>().InstancePerLifetimeScope();
        }
    }
}