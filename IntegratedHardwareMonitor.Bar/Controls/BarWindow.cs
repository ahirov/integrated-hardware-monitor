using System;
using System.ComponentModel;
using System.Windows;

using IntegratedHardwareMonitor.Bar.Entities;
using IntegratedHardwareMonitor.Bar.NativeCode;
using IntegratedHardwareMonitor.Bar.Services;

using Ninject;

namespace IntegratedHardwareMonitor.Bar.Controls
{
    public class BarWindow : Window
    {
        private static readonly Type s_windowType = typeof(BarWindow);
        private static readonly DependencyProperty s_monitor =
            DependencyProperty.Register(nameof(Monitor), typeof(MonitorInfo), s_windowType,
                new FrameworkPropertyMetadata(null, OnPositionChanged));

        private static readonly DependencyProperty s_position =
            DependencyProperty.Register(nameof(Position), typeof(BAR_POSITION), s_windowType,
                new FrameworkPropertyMetadata(BAR_POSITION.TOP, OnPositionChanged));

        private static readonly DependencyProperty s_thickness =
            DependencyProperty.Register(nameof(Thickness), typeof(int), s_windowType,
                new FrameworkPropertyMetadata(30, OnPositionChanged, OnThicknessAdjusted));

        private bool _isBarRegistered;
        private int _messageId;

        public BarWindow()
        {
            ShowInTaskbar = false;
            WindowStyle = WindowStyle.None;
            ResizeMode = ResizeMode.NoResize;
            Topmost = true;
        }

        [Inject]
        public IBarHandler BarHandler { get; set; }

        [Inject]
        public ISourceHandler SourceHandler { get; set; }

        [Inject]
        public IMessageHandler MessageHandler { get; set; }

        public bool IsBarResizing { get; set; }

        public MonitorInfo Monitor
        {
            get => (MonitorInfo)GetValue(s_monitor);
            set => SetValue(s_monitor, value);
        }

        public BAR_POSITION Position
        {
            get => (BAR_POSITION)GetValue(s_position);
            set => SetValue(s_position, value);
        }

        public int Thickness
        {
            get => (int)GetValue(s_thickness);
            set => SetValue(s_thickness, value);
        }

        public int MessageId
        {
            get
            {
                if (_messageId == 0)
                {
                    _messageId = User32.RegisterWindowMessage($"AppBarMessage_{Guid.NewGuid()}");
                }
                return _messageId;
            }
        }

        private static void OnPositionChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            BarWindow window = (BarWindow)obj;
            if (window._isBarRegistered)
            {
                window.BarHandler.UpdateBar(window);
            }
        }

        private static object OnThicknessAdjusted(DependencyObject obj, object baseValue)
        {
            BarWindow window = (BarWindow)obj;
            int value = (int)baseValue;
            return window.Position switch
            {
                BAR_POSITION.LEFT or BAR_POSITION.RIGHT
                => BarWindowHelper.LimitMinMax(value, window.MinWidth, window.MaxWidth),
                BAR_POSITION.TOP or BAR_POSITION.BOTTOM
                => BarWindowHelper.LimitMinMax(value, window.MinHeight, window.MaxHeight),
                _ => throw new NotSupportedException(),
            };
        }

        protected override void OnSourceInitialized(EventArgs args)
        {
            base.OnSourceInitialized(args);

            SourceHandler.RegisterHook(this);
            MessageHandler.SendMessage(this, AB_MSG.NEW);

            _isBarRegistered = true;
            BarHandler.UpdateBar(this);
        }

        protected override void OnDpiChanged(DpiScale oldDpi, DpiScale newDpi)
        {
            base.OnDpiChanged(oldDpi, newDpi);
            BarHandler.UpdateBar(this);
        }

        protected override void OnClosing(CancelEventArgs args)
        {
            base.OnClosing(args);
            if (_isBarRegistered)
            {
                MessageHandler.SendMessage(this, AB_MSG.REMOVE);
                _isBarRegistered = false;
            }
        }
    }
}
