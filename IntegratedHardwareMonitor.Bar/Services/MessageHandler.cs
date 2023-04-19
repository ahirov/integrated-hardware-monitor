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
        void SendMessage(BarWindow window, AbMsg message);
        void SendMessage(BarWindow window, AbMsg message, AppBarDataAction action, out AppBarData data);
        void SendMessage(AbMsg message, ref AppBarData data);
    }

    public sealed class MessageHandler : IMessageHandler
    {
        public void SendMessage(BarWindow window, AbMsg message)
        {
            SendMessage(window, message, null, out _);
        }

        public void SendMessage(BarWindow window, AbMsg message, AppBarDataAction action,
            out AppBarData data)
        {
            data = GetAppBarData(window);
            action?.Invoke(ref data);
            SendMessage(message, ref data);
        }

        public void SendMessage(AbMsg message, ref AppBarData data)
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
                Edge = (AbEdge)(int)window.Position
            };
        }
    }
}
