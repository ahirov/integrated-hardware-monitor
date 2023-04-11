using System;

namespace IntegratedHardwareMonitor.Bar.Entities
{
    /// <summary>
    /// More details here: https://www.pinvoke.net/default.aspx/user32/EnumDisplayMonitors.html
    /// </summary>
    internal delegate bool MonitorEnumDelegate(IntPtr monitor, IntPtr context,
        ref Rectangle rectangleContext, IntPtr data);
}
