using System.Windows;

using Autofac;

using IntegratedHardwareMonitor.Bar.IoC;
using IntegratedHardwareMonitor.Core.IoC;
using IntegratedHardwareMonitor.IoC;
using IntegratedHardwareMonitor.Windows;

namespace IntegratedHardwareMonitor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public sealed partial class App : Application
    {
        private IContainer _container;

        protected override void OnStartup(StartupEventArgs args)
        {
            base.OnStartup(args);
            ConfigureContainer();

            ComposeObjects();
            Current.MainWindow.Show();
        }

        private void ConfigureContainer()
        {
            ContainerBuilder builder = new();
            _ = builder.RegisterModule<BarModule>();
            _ = builder.RegisterModule<Commonmodule>();
            _ = builder.RegisterModule<ViewModule>();
            _container = builder.Build();
        }

        private void ComposeObjects()
        {
            Current.DispatcherUnhandledException
                += _container.Resolve<IExceptionHandler>().DisplayException;
            Current.MainWindow = _container.Resolve<MainWindow>();
        }
    }
}
