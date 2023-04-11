using System;

namespace IntegratedHardwareMonitor.Bar.Entities
{
    /// <summary>
    /// More details here: https://www.pinvoke.net/default.aspx/shell32/SHAppBarMessage.html
    /// </summary>
    public enum AB_EDGE
    {
        LEFT,
        TOP,
        RIGHT,
        BOTTOM
    }

    /// <summary>
    /// More details here: https://www.pinvoke.net/default.aspx/shell32/SHAppBarMessage.html
    /// </summary>
    public enum AB_MSG
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
    public enum AB_NOTIFY
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
    public enum MONITOR_INFO_F
    {
        PRIMARY = 1
    }
}
