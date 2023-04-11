using System.Windows;

using IntegratedHardwareMonitor.Bar.Controls;

namespace IntegratedHardwareMonitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public sealed partial class MainWindow : BarWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnCloseButtonClick(object sender, RoutedEventArgs args)
        {
            Close();
        }
    }
}
