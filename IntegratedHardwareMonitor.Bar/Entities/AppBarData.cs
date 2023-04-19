using System;
using System.Runtime.InteropServices;

namespace IntegratedHardwareMonitor.Bar.Entities
{
    /// <summary>
    /// More details here: http://pinvoke.net/default.aspx/shell32/APPBARDATA.html
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct AppBarData
    {
        public int Size { get; set; } // initialize this field using: Marshal.SizeOf(typeof(AppBarData));
        public IntPtr Handle { get; set; }
        public int MessageId { get; set; }
        public AbEdge Edge { get; set; }
        public Rectangle Rectangle { get; set; }
        public IntPtr Param { get; set; }
    }
}
