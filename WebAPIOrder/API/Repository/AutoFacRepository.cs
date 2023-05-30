using API.Repository.interfaces;
using Autofac;

namespace API.Repository
{
    public class AutoFacRepository : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>().InstancePerLifetimeScope();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>().InstancePerLifetimeScope();
            builder.RegisterType<OrderProductRepository>().As<IOrderProductRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ProductRepository>().As<IProductRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PaymentRepository>().As<IPaymentRepository>().InstancePerDependency();
        }
    }
}