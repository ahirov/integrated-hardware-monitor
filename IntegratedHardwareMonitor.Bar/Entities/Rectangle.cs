using System.Runtime.InteropServices;
using System.Windows;

namespace IntegratedHardwareMonitor.Bar.Entities
{
    /// <summary>
    /// Mode details here: https://www.pinvoke.net/default.aspx/Structures/RECT.html
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Rectangle
    {
        public int Left { get; set; }
        public int Top { get; set; }
        public int Right { get; set; }
        public int Bottom { get; set; }

        public Rectangle(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public static explicit operator Rect(Rectangle rectangle)
        {
            int width = rectangle.Right - rectangle.Left;
            int height = rectangle.Bottom - rectangle.Top;
            return new Rect(rectangle.Left, rectangle.Top, width, height);
        }

        public static explicit operator Rectangle(Rect rectangle)
        {
            return new Rectangle((int)rectangle.Left, (int)rectangle.Top,
                (int)rectangle.Right, (int)rectangle.Bottom);
        }
    }
}
