using Autofac.Extensions.DependencyInjection;
using PaymentAPI.infrastructure;
using PaymentAPI.infrastructure.MessageQueue;
using PaymentAPI.Repositories;
using PaymentAPI.Services;
using Autofac;

namespace PaymentAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuild =>
                {
                    webBuild.UseStartup<Startup>();
                }
            ).UseServiceProviderFactory(new AutofacServiceProviderFactory());
    }
    public class Startup
    {
        public Startup(IConfiguration config)
        {
            Config = config;
        }
        public IConfiguration Config { get; }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutoFacRepository());
            builder.RegisterModule(new AutoFacService());
            builder.RegisterModule(new AutoFacInfrastructure());
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHostedService<Consumer>();
            services.AddAuthorization();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nome do Projeto v1");
                c.RoutePrefix = string.Empty;
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}