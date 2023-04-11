using System;
using System.Windows;

namespace IntegratedHardwareMonitor.Bar.Entities
{
    public sealed class MonitorInfo : IEquatable<MonitorInfo>
    {
        public bool IsPrimary { get; }
        public string DeviceName { get; }
        public Rect WorkArea { get; }
        public Rect Viewport { get; }

        internal MonitorInfo(MonitorInfoEx mex)
        {
            IsPrimary = mex.Flags.HasFlag(MONITOR_INFO_F.PRIMARY);
            DeviceName = mex.DeviceName;
            WorkArea = (Rect)mex.WorkArea;
            Viewport = (Rect)mex.Monitor;
        }

        public static bool operator ==(MonitorInfo a, MonitorInfo b)
        {
            return a?.Equals(b) ?? false;
        }

        public static bool operator !=(MonitorInfo a, MonitorInfo b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return DeviceName;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as MonitorInfo);
        }

        public override int GetHashCode()
        {
            return DeviceName.GetHashCode();
        }

        public bool Equals(MonitorInfo other)
        {
            return DeviceName == other?.DeviceName;
        }
    }
}
