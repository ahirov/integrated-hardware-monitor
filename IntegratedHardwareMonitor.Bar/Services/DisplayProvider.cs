using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;

using IntegratedHardwareMonitor.Bar.Entities;
using IntegratedHardwareMonitor.Bar.NativeCode;

namespace IntegratedHardwareMonitor.Bar.Services
{
    public interface IDisplayProvider
    {
        DisplayInfo GetDisplay(string id);
        DisplayInfo GetDisplay(DisplayInfo display);
        List<DisplayInfo> GetDisplays();
    }

    public sealed class DisplayProvider : IDisplayProvider
    {
        public DisplayInfo GetDisplay(string id)
        {
            List<DisplayInfo> displays = GetDisplays();
            return id != null && displays.Any(display => display.Id == id)
                ? displays.First(display => display.Id == id)
                : GetDefault(displays);
        }

        public DisplayInfo GetDisplay(DisplayInfo display)
        {
            List<DisplayInfo> displays = GetDisplays();
            return display != null && displays.Contains(display)
                ? display
                : GetDefault(displays);
        }

        public List<DisplayInfo> GetDisplays()
        {
            List<DisplayInfo> displays = new();
            MonitorEnumDelegate callback = delegate (IntPtr monitor, IntPtr _, ref Rectangle _, IntPtr _)
            {
                MonitorInfoEx info = new()
                {
                    Size = Marshal.SizeOf(typeof(MonitorInfoEx))
                };
                if (User32.GetMonitorInfo(monitor, ref info))
                {
                    displays.Add(new DisplayInfo(info));
                    return true;
                }
                else
                {
                    throw new Win32Exception();
                }
            };
            _ = User32.EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, callback, IntPtr.Zero);
            return displays;
        }

        private static DisplayInfo GetDefault(List<DisplayInfo> displays)
        {
            return displays.First(display => display.IsPrimary);
        }
    }
}
