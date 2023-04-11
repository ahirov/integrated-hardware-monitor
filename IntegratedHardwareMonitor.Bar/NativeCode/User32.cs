using System;
using System.Runtime.InteropServices;

using IntegratedHardwareMonitor.Bar.Entities;

namespace IntegratedHardwareMonitor.Bar.NativeCode
{
    internal sealed class User32
    {
        private const string DLL_NAME = "user32.dll";

        /// <summary>
        /// More details here: https://www.pinvoke.net/default.aspx/user32/EnumDisplayMonitors.html
        /// </summary>
        [DllImport(DLL_NAME)]
        public static extern bool EnumDisplayMonitors(IntPtr context, IntPtr clip,
            MonitorEnumDelegate callback, IntPtr data);

        /// <summary>
        /// More details here: https://www.pinvoke.net/default.aspx/user32/GetMonitorInfo.html
        /// </summary>
        [DllImport(DLL_NAME, CharSet = CharSet.Unicode)]
        public static extern bool GetMonitorInfo(IntPtr monitor, ref MonitorInfoEx info);

        /// <summary>
        /// More details here: https://www.pinvoke.net/default.aspx/user32/RegisterWindowMessage.html
        /// </summary>
        [DllImport(DLL_NAME, CharSet = CharSet.Unicode)]
        public static extern int RegisterWindowMessage(string message);
    }
}
