using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;

using IntegratedHardwareMonitor.Bar.Entities;
using IntegratedHardwareMonitor.Bar.NativeCode;

namespace IntegratedHardwareMonitor.Bar.Services
{
    public interface IMonitorProvider
    {
        MonitorInfo GetMonitor(MonitorInfo monitor);
    }

    public sealed class MonitorProvider : IMonitorProvider
    {
        public MonitorInfo GetMonitor(MonitorInfo monitor)
        {
            List<MonitorInfo> allMonitors = GetMonitors();
            if (monitor == null || !allMonitors.Contains(monitor))
            {
                monitor = allMonitors.First(f => f.IsPrimary);
            }
            return monitor;
        }

        private static List<MonitorInfo> GetMonitors()
        {
            List<MonitorInfo> monitors = new();
            MonitorEnumDelegate callback = delegate (IntPtr monitor, IntPtr _, ref Rectangle _, IntPtr _)
            {
                MonitorInfoEx info = new()
                {
                    Size = Marshal.SizeOf(typeof(MonitorInfoEx))
                };
                if (User32.GetMonitorInfo(monitor, ref info))
                {
                    monitors.Add(new MonitorInfo(info));
                    return true;
                }
                else
                {
                    throw new Win32Exception();
                }
            };
            _ = User32.EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, callback, IntPtr.Zero);
            return monitors;
        }
    }
}
