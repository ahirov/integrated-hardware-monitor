using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Animation;

namespace IntegratedHardwareMonitor.Windows
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class SettingWindow : Window
    {
        private readonly int _animationDurationMs = 200; // milliseconds
        public string AnimationDuration => $"0:0:{_animationDurationMs / 1000d}";
        public static int FullOpacity => 1;
        public static int ZeroOpacity => 0;

        public SettingWindow()
        {
            DataContext = this;
            InitializeComponent();
        }

        private void OnSaveButtonClick(object sender, RoutedEventArgs args)
        {
            Close();
        }

        private void OnCloseButtonClick(object sender, RoutedEventArgs args)
        {
            Close();
        }

        private void OnWindowClosing(object sender, CancelEventArgs args)
        {
            args.Cancel = true;
            DoubleAnimation animation = new(ZeroOpacity,
                TimeSpan.FromMilliseconds(_animationDurationMs));

            animation.Completed += (_, _) => Close();
            BeginAnimation(OpacityProperty, animation);
        }
    }
}
