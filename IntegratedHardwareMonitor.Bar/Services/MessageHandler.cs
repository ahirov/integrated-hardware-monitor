using System.Runtime.InteropServices;
using System.Windows.Interop;

using IntegratedHardwareMonitor.Bar.Controls;
using IntegratedHardwareMonitor.Bar.Entities;
using IntegratedHardwareMonitor.Bar.NativeCode;

namespace IntegratedHardwareMonitor.Bar.Services
{
    public delegate void AppBarDataAction(ref AppBarData item);

    public interface IMessageHandler
    {
        void SendMessage(BarWindow window, AB_MSG message);
        void SendMessage(BarWindow window, AB_MSG message, AppBarDataAction action, out AppBarData data);
        void SendMessage(AB_MSG message, ref AppBarData data);
    }

    public sealed class MessageHandler : IMessageHandler
    {
        public void SendMessage(BarWindow window, AB_MSG message)
        {
            SendMessage(window, message, null, out _);
        }

        public void SendMessage(BarWindow window, AB_MSG message, AppBarDataAction action,
            out AppBarData data)
        {
            data = GetAppBarData(window);
            action?.Invoke(ref data);
            SendMessage(message, ref data);
        }

        public void SendMessage(AB_MSG message, ref AppBarData data)
        {
            _ = Shell32.SHAppBarMessage(message, ref data);
        }

        private static AppBarData GetAppBarData(BarWindow window)
        {
            return new AppBarData()
            {
                Size = Marshal.SizeOf(typeof(AppBarData)),
                Handle = new WindowInteropHelper(window).Handle,
                MessageId = window.MessageId,
                Edge = (AB_EDGE)(int)window.Position
            };
        }
    }
}
