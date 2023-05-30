using PaymentAPI.Services.Interface;
using PaymentAPI.Services;

namespace Tests.Services
{
    public class AutoFacServiceTests : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PaymentCARD>().As<IPaymentCARD>().InstancePerLifetimeScope();
            builder.RegisterType<PaymentPIX>().As<IPaymentPIX>().InstancePerLifetimeScope();
            builder.RegisterType<PaymentTicket>().As<IPaymentTicket>().InstancePerLifetimeScope();
        }
    }
}