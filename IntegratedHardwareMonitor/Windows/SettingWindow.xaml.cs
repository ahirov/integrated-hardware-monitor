using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

using IntegratedHardwareMonitor.Bar.Controls;
using IntegratedHardwareMonitor.Bar.Services;
using IntegratedHardwareMonitor.Core.Entities;
using IntegratedHardwareMonitor.Core.Entities.Enumerations;
using IntegratedHardwareMonitor.Core.Services;
using IntegratedHardwareMonitor.IoC;

using Wpf.Ui.Appearance;

namespace IntegratedHardwareMonitor.Windows
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

        public ObservableCollection<HardwareComponent> SelectedItems { get; set; }
        public ObservableCollection<HardwareComponent> TotalItems { get; set; }

        public SettingWindow(MainWindow barWindow, ISettingWindowDependencies settingDependencies)
        {
            _barWindow = barWindow;
            _settingDependencies = settingDependencies;

            SelectedItems = new ObservableCollection<HardwareComponent>();
            TotalItems = new ObservableCollection<HardwareComponent>();
            for (int i = 0; i < 20; i++)
            {
                TotalItems.Add(new HardwareComponent()
                {
                    Position = i,
                    Value = (i + 1).ToString()
                });
            }
            InitializeComponent();
        }

        protected override void OnSourceInitialized(EventArgs args)
        {
            base.OnSourceInitialized(args);
            ISettingHandler settingHandler = _settingDependencies.SettingHandler;
            IDisplayProvider displayProvider = _settingDependencies.DisplayProvider;

            ThemeCmbBx.ItemsSource = settingHandler.GetThemes();
            ThemeCmbBx.SelectedValue = settingHandler.Setting.Design;

            PositionCmbBx.ItemsSource = settingHandler.GetPositions();
            PositionCmbBx.SelectedValue = settingHandler.Setting.Position;

            DisplayCmbBx.ItemsSource = displayProvider.GetDisplays();
            DisplayCmbBx.SelectedValue = displayProvider.GetDisplay(settingHandler.Setting.DisplayId).Id;

            AnimationElement.Duration = AnimationDuration;
            AnimationElement.From = ZeroOpacity;
            AnimationElement.To = FullOpacity;
            ComponentsLstSltr.DataContext = new
            {
                SelectedItems,
                TotalItems
            };
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

        private void OnChangeThemeCmbBx(object sender, SelectionChangedEventArgs args)
        {
            Design design = (Design)ThemeCmbBx.SelectedValue;
            Theme.Apply(_settingDependencies.Mapper.Map<ThemeType>(design));
        }

        private void OnChangePositionCmbBx(object sender, SelectionChangedEventArgs args)
        {
            Position position = (Position)PositionCmbBx.SelectedValue;
            _barWindow.Position = _settingDependencies.Mapper.Map<BarPosition>(position);
        }

        private void OnChangeDisplayCmbBx(object sender, SelectionChangedEventArgs args)
        {
            string id = (string)DisplayCmbBx.SelectedValue;
            _barWindow.Display = _settingDependencies.DisplayProvider.GetDisplay(id);
        }

        private void OnClickSaveBtn(object sender, RoutedEventArgs args)
        {
            _settingDependencies.SettingHandler.Save(new ApplicationSetting()
            {
                Design = (Design)ThemeCmbBx.SelectedValue,
                Position = (Position)PositionCmbBx.SelectedValue,
                DisplayId = (string)DisplayCmbBx.SelectedValue
            });
            Close();
        }

        private void OnClickCancelBtn(object sender, RoutedEventArgs args)
        {
            ApplicationSetting setting = _settingDependencies.SettingHandler.Setting;
            ThemeCmbBx.SelectedValue = setting.Design;
            PositionCmbBx.SelectedValue = setting.Position;
            DisplayCmbBx.SelectedValue = setting.DisplayId;
            Close();
        }
    }
}
