using System;

namespace IntegratedHardwareMonitor.Bar.Entities
{
    /// <summary>
    /// More details here: https://www.pinvoke.net/default.aspx/shell32/SHAppBarMessage.html
    /// </summary>
    public enum AbEdge
    {
        LEFT,
        TOP,
        RIGHT,
        BOTTOM
    }

    /// <summary>
    /// More details here: https://www.pinvoke.net/default.aspx/shell32/SHAppBarMessage.html
    /// </summary>
    public enum AbMsg
    {
        NEW,
        REMOVE,
        QUERYPOS,
        SETPOS,
        GETSTATE,
        GETTASKBARPOS,
        ACTIVATE,
        GETAUTOHIDEBAR,
        SETAUTOHIDEBAR,
        WINDOWPOSCHANGED,
        SETSTATE
    }

    /// <summary>
    /// More details here: https://www.pinvoke.net/default.aspx/shell32/SHAppBarMessage.html
    /// </summary>
    public enum AbNotify
    {
        STATECHANGE,
        POSCHANGED,
        FULLSCREENAPP,
        WINDOWARRANGE
    }

    /// <summary>
    /// More details here: https://www.pinvoke.net/default.aspx/user32/MONITORINFOEX.html
    /// </summary>
    [Flags]
    public enum MonitorInfoF
    {
        PRIMARY = 1
    }
}
