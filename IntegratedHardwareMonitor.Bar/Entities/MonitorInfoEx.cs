using System.Runtime.InteropServices;

namespace IntegratedHardwareMonitor.Bar.Entities
{
    /// <summary>
    /// More details here: https://www.pinvoke.net/default.aspx/user32/MONITORINFOEX.html
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct MonitorInfoEx
    {
        private const int CCH_DEVICE_NAME = 32;

#pragma warning disable IDE0032 // Use auto property
        private int _size; // initialize this field using: Marshal.SizeOf(typeof(MonitorInfoEx));
        private Rectangle _monitor;
        private Rectangle _workArea;
        private readonly MONITOR_INFO_F _flags;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCH_DEVICE_NAME)]
        private readonly string _deviceName;
#pragma warning restore IDE0032 // Use auto property

        public int Size { get => _size; set => _size = value; }
        public Rectangle Monitor => _monitor;
        public Rectangle WorkArea => _workArea;
        public MONITOR_INFO_F Flags => _flags;
        public string DeviceName => _deviceName;
    }
}
