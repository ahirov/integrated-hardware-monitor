using System;
using System.Windows;

using IntegratedHardwareMonitor.Bar.Controls;
using IntegratedHardwareMonitor.Bar.IoC;
using IntegratedHardwareMonitor.Common.Entities;
using IntegratedHardwareMonitor.View.IoC;

using Wpf.Ui.Appearance;

namespace IntegratedHardwareMonitor.View.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public sealed partial class MainWindow : BarWindow
    {
        private readonly ISettingWindowDependencies _settingDependencies;
        private Window _setting;

        public MainWindow(ISettingWindowDependencies settingDependencies,
            IBarWindowDependencies barDependencies)
            : base(barDependencies)
        {
            _settingDependencies = settingDependencies;
            InitializeComponent();
        }

        protected override void OnSourceInitialized(EventArgs args)
        {
            base.OnSourceInitialized(args);

            Design design = _settingDependencies.SettingHandler.Setting.Design;
            ThemeType theme = _settingDependencies.Mapper.Map<ThemeType>(design);
            Theme.Apply(theme);
        }

        private void OnClickSettingBtn(object sender, RoutedEventArgs args)
        {
            if (_setting == null || !_setting.IsLoaded)
            {
                _setting = new SettingWindow(this, _settingDependencies);
                _setting.Show();
            }
        }

        private void OnClickCloseBtn(object sender, RoutedEventArgs args)
        {
            Close();
        }
    }
}
