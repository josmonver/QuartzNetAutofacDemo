using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.Owin;
using Owin;
using Quartz;
using Quartz.Impl;
using System.Reflection;
using System.Web.Http;

[assembly: OwinStartupAttribute(typeof(QuartzNetAutofacDemo.Startup))]
namespace QuartzNetAutofacDemo
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            IContainer container = null;
            var builder = new ContainerBuilder();

            // Schedule
            builder.Register(x => new StdSchedulerFactory().GetScheduler()).As<IScheduler>();
            // Jobs
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).Where(x => typeof(IJob).IsAssignableFrom(x));
            // Servies
            builder.RegisterType<FooService>().As<IFooService>().InstancePerLifetimeScope();

            // Resolve dependencies
            container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            //Schedule
            IScheduler scheduler = container.Resolve<IScheduler>();
            scheduler.JobFactory = new AutofacJobFactory(container);

            scheduler.ScheduleJob(
                JobBuilder.Create<FooJob>().Build(),
                TriggerBuilder.Create().WithSimpleSchedule(s => s.WithIntervalInSeconds(5).RepeatForever()).Build());

            scheduler.Start();
        }
    }
}
