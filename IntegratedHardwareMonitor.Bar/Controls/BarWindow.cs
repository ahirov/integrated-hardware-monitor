using System;
using System.ComponentModel;
using System.Windows;

using IntegratedHardwareMonitor.Bar.Entities;
using IntegratedHardwareMonitor.Bar.IoC;
using IntegratedHardwareMonitor.Bar.NativeCode;
using IntegratedHardwareMonitor.Bar.Services;
using IntegratedHardwareMonitor.Core.Entities;

namespace IntegratedHardwareMonitor.Bar.Controls
{
    public class BarWindow : Window
    {
        private static readonly Type s_windowType = typeof(BarWindow);
        private readonly IBarWindowDependencies _dependencies;

        private int _messageId;
        private DependencyProperty _display;
        private DependencyProperty _position;
        private DependencyProperty _thickness;

        public bool IsBarRegistered { get; set; }
        public bool IsBarResizing { get; set; }

        public DisplayInfo Display
        {
            get => (DisplayInfo)GetValue(_display);
            set => SetValue(_display, value);
        }

        public BarPosition Position
        {
            get => (BarPosition)GetValue(_position);
            set => SetValue(_position, value);
        }

        public int Thickness
        {
            get => (int)GetValue(_thickness);
            set => SetValue(_thickness, value);
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

        public BarWindow(IBarWindowDependencies dependencies)
        {
            _dependencies = dependencies;
            InitializeProperties();

            ShowInTaskbar = false;
            WindowStyle = WindowStyle.None;
            ResizeMode = ResizeMode.NoResize;
            Topmost = true;
        }

        protected override void OnSourceInitialized(EventArgs args)
        {
            base.OnSourceInitialized(args);

            _dependencies.SourceHandler.RegisterHook(this);
            _dependencies.MessageHandler.SendMessage(this, AbMsg.NEW);

            IsBarRegistered = true;
            UpdateBar();
        }

        protected override void OnDpiChanged(DpiScale oldDpi, DpiScale newDpi)
        {
            base.OnDpiChanged(oldDpi, newDpi);
            UpdateBar();
        }

        protected override void OnClosing(CancelEventArgs args)
        {
            base.OnClosing(args);
            if (IsBarRegistered)
            {
                _dependencies.MessageHandler.SendMessage(this, AbMsg.REMOVE);
                IsBarRegistered = false;
            }
        }

        public void UpdateBar()
        {
            _dependencies.BarHandler.UpdateBar(this);
        }

        private void InitializeProperties()
        {
            IBarEventsHandler handler = _dependencies.BarEventsHandler;
            ApplicationSetting setting = _dependencies.SettingProvider.Setting;

            DisplayInfo display = _dependencies.DisplayProvider.GetDisplay(setting.DisplayId);
            _display = DependencyProperty.Register(nameof(Display), typeof(DisplayInfo), s_windowType,
                new FrameworkPropertyMetadata(display, handler.OnPositionChanged));

            BarPosition position = _dependencies.Mapper.Map<BarPosition>(setting.Position);
            _position = DependencyProperty.Register(nameof(Position), typeof(BarPosition), s_windowType,
                new FrameworkPropertyMetadata(position, handler.OnPositionChanged));

            int defaultThickness = 30;
            _thickness = DependencyProperty.Register(nameof(Thickness), typeof(int), s_windowType,
                new FrameworkPropertyMetadata(defaultThickness,
                handler.OnPositionChanged, handler.OnThicknessAdjusted));
        }
    }
}
