using System;
using System.Runtime.InteropServices;

namespace IntegratedHardwareMonitor.Bar.Entities
{
    /// <summary>
    /// Mode details here: https://www.pinvoke.net/default.aspx/Structures/WINDOWPOS.html
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct WindowPos
    {
        public IntPtr Handle { get; set; }
        public IntPtr HandleInsertAfter { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Cx { get; set; }
        public int Cy { get; set; }
        public int Flags { get; set; }
    }
}
