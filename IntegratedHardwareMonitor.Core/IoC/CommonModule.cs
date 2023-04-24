using Autofac;

using IntegratedHardwareMonitor.Core.Services;

namespace IntegratedHardwareMonitor.Core.IoC
{
    public sealed class Commonmodule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            _ = builder.RegisterType<SettingHandler>().As<ISettingHandler>().SingleInstance();
            _ = builder.RegisterType<SettingStore>().As<ISettingStore>().SingleInstance();
        }
    }
}
