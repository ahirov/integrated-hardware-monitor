using IntegratedHardwareMonitor.Bar.Services;

using Ninject.Modules;

namespace IntegratedHardwareMonitor.Bar.IoC
{
    public sealed class BarModule : NinjectModule
    {
        public override void Load()
        {
            _ = Bind<IMonitorProvider>().To<MonitorProvider>().InSingletonScope();
            _ = Bind<IMessageHandler>().To<MessageHandler>().InSingletonScope();
            _ = Bind<ISourceHandler>().To<SourceHandler>().InSingletonScope();
            _ = Bind<IBarHandler>().To<BarHandler>().InSingletonScope();
        }
    }
}
