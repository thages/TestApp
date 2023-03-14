using Autofac;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SpecFlow.Autofac;
using System;
using System.Linq;
using TechTalk.SpecFlow;
using TestApp.Adapters.EntityFramework;
using TestApp.Adapters.EntityFramework.Repositories;
using TestApp.Domain.Repositories;

namespace TestApp.Test.Supporte
{
    public class AutofacDependencies
    {
        [ScenarioDependencies]
        public static ContainerBuilder CreateContainerBuilder()
        {
            var builder = new ContainerBuilder();

            var configuration = new ConfigurationBuilder()
                                    .Build();

            builder.Register(c => configuration).As<IConfiguration>();

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
            var optionsBuilder = new DbContextOptionsBuilder<TestAppContext>();

            optionsBuilder.UseInMemoryDatabase("StartApp");

            builder.Register(x =>
            {
                return new TestAppContext(optionsBuilder.Options);
            }).As<DbContext>().InstancePerLifetimeScope();

            #endregion

            #region Repositories
            builder.RegisterType<PerformanceIndicatorRepository>().As<IPerformanceIndicatorRepository>()
                .WithParameter("context", new TestAppContext(optionsBuilder.Options))
                .InstancePerLifetimeScope();

            builder.RegisterType<AverageIndicatorRepository>().As<IAverageIndicatorRepository>()
                .WithParameter("context", new TestAppContext(optionsBuilder.Options))
                .InstancePerLifetimeScope();

            builder.RegisterType<SumIndicatorRepository>().As<ISumIndicatorRepository>()
                .WithParameter("context", new TestAppContext(optionsBuilder.Options))
                .InstancePerLifetimeScope();
            #endregion

            builder.RegisterTypes(typeof(AutofacDependencies).Assembly.GetTypes().Where(t => Attribute.IsDefined(t, typeof(BindingAttribute))).ToArray()).InstancePerLifetimeScope();


            return builder;
        }
    }
}
