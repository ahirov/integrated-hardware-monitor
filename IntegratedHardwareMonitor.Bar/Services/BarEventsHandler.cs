using System;
using System.Windows;

using IntegratedHardwareMonitor.Bar.Controls;

namespace IntegratedHardwareMonitor.Bar.Services
{
    public interface IBarEventsHandler
    {
        void OnPositionChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args);
        object OnThicknessAdjusted(DependencyObject obj, object baseValue);
    }

    public sealed class BarEventsHandler : IBarEventsHandler
    {
        public void OnPositionChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            BarWindow window = (BarWindow)obj;
            if (window.IsBarRegistered)
            {
                window.UpdateBar();
            }
        }

        public object OnThicknessAdjusted(DependencyObject obj, object baseValue)
        {
            BarWindow window = (BarWindow)obj;
            int value = (int)baseValue;
            return window.Position switch
            {
                BarPosition.LEFT or BarPosition.RIGHT
                => BarWindowHelper.LimitMinMax(value, window.MinWidth, window.MaxWidth),
                BarPosition.TOP or BarPosition.BOTTOM
                => BarWindowHelper.LimitMinMax(value, window.MinHeight, window.MaxHeight),
                _ => throw new NotSupportedException(),
            };
        }
    }
}
