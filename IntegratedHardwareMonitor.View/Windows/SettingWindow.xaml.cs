using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

using IntegratedHardwareMonitor.Bar.Controls;
using IntegratedHardwareMonitor.Bar.Services;
using IntegratedHardwareMonitor.Common.Entities;
using IntegratedHardwareMonitor.Common.Services;
using IntegratedHardwareMonitor.View.IoC;

using Wpf.Ui.Appearance;

namespace IntegratedHardwareMonitor.View.Windows
{
    /// <summary>
    /// Interaction logic for SettingWindow.xaml
    /// </summary>
    public sealed partial class SettingWindow : Window
    {
        private static TimeSpan AnimationDuration => TimeSpan.FromMilliseconds(200);
        private static int FullOpacity => 1;
        private static int ZeroOpacity => 0;

        private readonly ISettingWindowDependencies _settingDependencies;
        private readonly MainWindow _barWindow;
        private bool _isClosed;

        public SettingWindow(MainWindow barWindow, ISettingWindowDependencies settingDependencies)
        {
            _barWindow = barWindow;
            _settingDependencies = settingDependencies;
            InitializeComponent();
        }

        protected override void OnSourceInitialized(EventArgs args)
        {
            base.OnSourceInitialized(args);
            ISettingHandler settingHandler = _settingDependencies.SettingHandler;
            IDisplayProvider displayProvider = _settingDependencies.DisplayProvider;

            Themes.ItemsSource = settingHandler.GetThemes();
            Themes.SelectedValue = settingHandler.Setting.Design;

            Positions.ItemsSource = settingHandler.GetPositions();
            Positions.SelectedValue = settingHandler.Setting.Position;

            Displays.ItemsSource = displayProvider.GetDisplays();
            Displays.SelectedValue = displayProvider.GetDisplay(settingHandler.Setting.DisplayId).Id;

            AnimationElement.Duration = AnimationDuration;
            AnimationElement.From = ZeroOpacity;
            AnimationElement.To = FullOpacity;
        }

        protected override void OnClosing(CancelEventArgs args)
        {
            if (_isClosed)
            {
                base.OnClosing(args);
            }
            else
            {
                args.Cancel = true;
                DoubleAnimation animation = new(ZeroOpacity, AnimationDuration);

                animation.Completed += (_, _) =>
                {
                    _isClosed = true;
                    Close();
                };
                BeginAnimation(OpacityProperty, animation);
            }
        }

        private void OnThemeChange(object sender, SelectionChangedEventArgs args)
        {
            Design design = (Design)Themes.SelectedValue;
            Theme.Apply(_settingDependencies.Mapper.Map<ThemeType>(design));
        }

        private void OnPositionChange(object sender, SelectionChangedEventArgs args)
        {
            Position position = (Position)Positions.SelectedValue;
            _barWindow.Position = _settingDependencies.Mapper.Map<BarPosition>(position);
        }

        private void OnDisplayChange(object sender, SelectionChangedEventArgs args)
        {
            string id = (string)Displays.SelectedValue;
            _barWindow.Display = _settingDependencies.DisplayProvider.GetDisplay(id);
        }

        private void OnSaveButtonClick(object sender, RoutedEventArgs args)
        {
            _settingDependencies.SettingHandler.Save(new Setting()
            {
                Design = (Design)Themes.SelectedValue,
                Position = (Position)Positions.SelectedValue,
                DisplayId = (string)Displays.SelectedValue
            });
            Close();
        }

        private void OnCancelButtonClick(object sender, RoutedEventArgs args)
        {
            Setting setting = _settingDependencies.SettingHandler.Setting;
            Themes.SelectedValue = setting.Design;
            Positions.SelectedValue = setting.Position;
            Displays.SelectedValue = setting.DisplayId;
            Close();
        }
    }
}
