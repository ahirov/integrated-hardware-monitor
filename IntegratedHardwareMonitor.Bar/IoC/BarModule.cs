using Autofac;
using Autofac.Extras.AggregateService;

using AutoMapper.Contrib.Autofac.DependencyInjection;

using IntegratedHardwareMonitor.Bar.Services;

namespace IntegratedHardwareMonitor.Bar.IoC
{
    public sealed class BarModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            _ = builder.RegisterAutoMapper(typeof(BarModule).Assembly);
            builder.RegisterAggregateService<IBarWindowDependencies>();
            _ = builder.RegisterType<DisplayProvider>().As<IDisplayProvider>().SingleInstance();
            _ = builder.RegisterType<MessageHandler>().As<IMessageHandler>().SingleInstance();
            _ = builder.RegisterType<SourceHandler>().As<ISourceHandler>().SingleInstance();
            _ = builder.RegisterType<BarHandler>().As<IBarHandler>().SingleInstance();
            _ = builder.RegisterType<BarEventsHandler>().As<IBarEventsHandler>().SingleInstance();
        }
    }
}
