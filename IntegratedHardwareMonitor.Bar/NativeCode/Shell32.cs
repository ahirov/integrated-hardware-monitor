using System.Runtime.InteropServices;

using IntegratedHardwareMonitor.Bar.Entities;

namespace IntegratedHardwareMonitor.Bar.NativeCode
{
    internal sealed class Shell32
    {
        private const string DLL_NAME = "shell32.dll";

        /// <summary>
        /// More details here: https://www.pinvoke.net/default.aspx/shell32/SHAppBarMessage.html
        /// </summary>
        [DllImport(DLL_NAME)]
        public static extern uint SHAppBarMessage(AbMsg message, ref AppBarData data);
    }
}
