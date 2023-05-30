using Tests.Infraestructure.Context;
using Tests.Repository;
using Tests.Services;
using Xunit.Abstractions;

[assembly: TestFramework("Tests.ConfigureTestFramework", "Tests")]
namespace Tests
{
    public class ConfigureTestFramework : AutofacTestFramework
    {
        public ConfigureTestFramework(IMessageSink diagnosticMessageSink)
            : base(diagnosticMessageSink)
        {
        }
        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutoFacApplicationContextTests());
            builder.RegisterModule(new AutoFacServicesTest());
            builder.RegisterModule(new AutoFacRepositoriesTest());
        }

    }
}