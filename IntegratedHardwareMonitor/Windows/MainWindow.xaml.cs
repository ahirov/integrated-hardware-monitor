using System.Windows;

using IntegratedHardwareMonitor.Bar.Controls;
using IntegratedHardwareMonitor.Windows;

using Wpf.Ui.Appearance;

namespace IntegratedHardwareMonitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public sealed partial class MainWindow : BarWindow
    {
        public MainWindow()
        {
            Theme.Apply(ThemeType.Light);
            InitializeComponent();
        }

        private void OnSettingButtonClick(object sender, RoutedEventArgs args)
        {
            Window window = new SettingWindow();
            window.Show();
        }

        private void OnCloseButtonClick(object sender, RoutedEventArgs args)
        {
            Close();
        }
    }
}
