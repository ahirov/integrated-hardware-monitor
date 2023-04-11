using System.Windows;

using IntegratedHardwareMonitor.Bar.IoC;

using Ninject;

namespace IntegratedHardwareMonitor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IKernel _container;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureContainer();

            ComposeObjects();
            Current.MainWindow.Show();
        }

        private void ConfigureContainer()
        {
            _container = new StandardKernel(new BarModule());
        }

        private void ComposeObjects()
        {
            Current.MainWindow = _container.Get<MainWindow>();
        }
    }
}
