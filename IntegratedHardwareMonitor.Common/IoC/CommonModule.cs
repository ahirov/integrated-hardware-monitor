using Autofac;

using IntegratedHardwareMonitor.Common.Services;
using IntegratedHardwareMonitor.View.Services;

namespace IntegratedHardwareMonitor.Common.IoC
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
