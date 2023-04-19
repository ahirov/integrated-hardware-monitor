using System;
using System.Windows;
using System.Windows.Media;

using IntegratedHardwareMonitor.Bar.Controls;
using IntegratedHardwareMonitor.Bar.Entities;

namespace IntegratedHardwareMonitor.Bar.Services
{
    public interface IBarHandler
    {
        void UpdateBar(BarWindow window);
    }

    public sealed class BarHandler : IBarHandler
    {
        private readonly IMessageHandler _messageHandler;
        private readonly IDisplayProvider _displayProvider;

        public BarHandler(IMessageHandler messageHandler, IDisplayProvider displayProvider)
        {
            _messageHandler = messageHandler;
            _displayProvider = displayProvider;
        }

        public void UpdateBar(BarWindow window)
        {
            if (window.IsBarResizing)
            {
                return;
            }

            AppBarDataAction action = (ref AppBarData data)
                => data.Rectangle = (Rectangle)_displayProvider.GetDisplay(window.Display).Viewport;
            _messageHandler.SendMessage(window, AbMsg.QUERYPOS, action, out AppBarData data);

            data.Rectangle = GetNewRectangle(data.Rectangle, window);
            _messageHandler.SendMessage(AbMsg.SETPOS, ref data);

            window.IsBarResizing = true;
            try
            {
                Rect rect = (Rect)data.Rectangle;
                window.Left = DesktopDimensionToWpf(window, rect.Left);
                window.Top = DesktopDimensionToWpf(window, rect.Top);
                window.Width = DesktopDimensionToWpf(window, rect.Width);
                window.Height = DesktopDimensionToWpf(window, rect.Height);
            }
            finally
            {
                window.IsBarResizing = false;
            }
        }

        private static Rectangle GetNewRectangle(Rectangle rectangle, BarWindow window)
        {
            int thickness = WpfDimensionToDesktop(window, window.Thickness);
            switch (window.Position)
            {
                case BarPosition.TOP:
                    rectangle.Bottom = rectangle.Top + thickness;
                    break;
                case BarPosition.BOTTOM:
                    rectangle.Top = rectangle.Bottom - thickness;
                    break;
                case BarPosition.LEFT:
                    rectangle.Right = rectangle.Left + thickness;
                    break;
                case BarPosition.RIGHT:
                    rectangle.Left = rectangle.Right - thickness;
                    break;
                default: throw new NotSupportedException();
            }
            return rectangle;
        }

        private static int WpfDimensionToDesktop(Window window, double dim)
        {
            DpiScale dpi = VisualTreeHelper.GetDpi(window);
            return (int)Math.Ceiling(dim * dpi.PixelsPerDip);
        }

        private static double DesktopDimensionToWpf(Window window, double dim)
        {
            DpiScale dpi = VisualTreeHelper.GetDpi(window);
            return dim / dpi.PixelsPerDip;
        }
    }
}
