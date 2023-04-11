using System;

namespace IntegratedHardwareMonitor.Bar.Controls
{
    internal sealed class BarWindowHelper
    {
        public static int LimitMinMax(int value, double min, double max)
        {
            int result = value;
            if (value < min)
            {
                result = (int)Math.Ceiling(min);
            }
            if (value > max)
            {
                result = (int)Math.Floor(max);
            }
            return result;
        }
    }
}
