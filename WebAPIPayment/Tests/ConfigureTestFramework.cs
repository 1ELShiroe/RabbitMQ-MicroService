using Tests.infrastructure;
using Tests.Repositories;
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
            builder.RegisterModule(new AutoFacInfrastructureTests());
            builder.RegisterModule(new AutoFacRepositoryTests());
            builder.RegisterModule(new AutoFacServiceTests());
        }

    }
}