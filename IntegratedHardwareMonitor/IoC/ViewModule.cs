using Autofac;
using Autofac.Extras.AggregateService;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using IntegratedHardwareMonitor.Windows;

namespace IntegratedHardwareMonitor.IoC
{
    public sealed class ViewModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            _ = builder.RegisterAutoMapper(typeof(App).Assembly);
            builder.RegisterAggregateService<ISettingWindowDependencies>();
            _ = builder.RegisterType<ExceptionHandler>().As<IExceptionHandler>().SingleInstance();
            _ = builder.RegisterType<MainWindow>().SingleInstance();
        }
    }
}
