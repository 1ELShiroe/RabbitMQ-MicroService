using PaymentAPI.Repositories;
using PaymentAPI.Repositories.interfaces;

namespace Tests.Repositories
{
    public class AutoFacRepositoryTests : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PaymentRepository>().As<IPaymentRepository>().InstancePerLifetimeScope();
        }
    }
}