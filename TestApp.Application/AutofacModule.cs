using Autofac;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TestApp.Adapters.EntityFramework;
using TestApp.Adapters.EntityFramework.Repositories;
using TestApp.Domain.Repositories;

namespace TestApp.Application
{
    public class AutofacModule : Module
    {
        private readonly IConfiguration Configuration;

        public AutofacModule(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var applicationAssembly = AppDomain.CurrentDomain.Load($"TestApp.Application");
            var domainAssembly = AppDomain.CurrentDomain.Load($"TestApp.Domain");
            var efAssembly = AppDomain.CurrentDomain.Load($"TestApp.Adapters.EntityFramework");

            #region MediatR
            var openHandlersType = new[] { typeof(IRequestHandler<,>), typeof(INotificationHandler<>) };
            foreach (var openHandler in openHandlersType)
            {
                builder.RegisterAssemblyTypes(applicationAssembly)
                    .AsClosedTypesOf(openHandler)
                    .InstancePerDependency();
            }

            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
            #endregion

            #region EF
            var optionBuilder = new DbContextOptionsBuilder<TestAppContext>();

            optionBuilder.UseSqlServer(Configuration["ConnectionStrings:SqlDatabase"]);

            builder.Register(x =>
            {
                return new TestAppContext(optionBuilder.Options);
            }).As<DbContext>().InstancePerLifetimeScope();

            new TestAppContext(optionBuilder.Options).Database.Migrate();
            #endregion

            #region Repositories
            builder.RegisterType<PerformanceIndicatorRepository>().As<IPerformanceIndicatorRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AverageIndicatorRepository>().As<IAverageIndicatorRepository>().InstancePerLifetimeScope();
            builder.RegisterType<SumIndicatorRepository>().As<ISumIndicatorRepository>().InstancePerLifetimeScope();
            #endregion

        }
    }
}