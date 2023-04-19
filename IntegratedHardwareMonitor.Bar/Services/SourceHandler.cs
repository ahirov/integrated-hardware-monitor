using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

using IntegratedHardwareMonitor.Bar.Controls;
using IntegratedHardwareMonitor.Bar.Entities;

namespace IntegratedHardwareMonitor.Bar.Services
{
    public interface ISourceHandler
    {
        void RegisterHook(BarWindow window);
    }

    public sealed class SourceHandler : ISourceHandler
    {
        private const int
            SWP_NOSIZE = 0x0001,
            SWP_NOMOVE = 0x0002,
            WM_ACTIVATE = 0x0006,
            WM_WINDOWPOSCHANGING = 0x0046,
            WM_WINDOWPOSCHANGED = 0x0047;

        private readonly IMessageHandler _messageHandler;
        private readonly IBarHandler _barHandler;

        public SourceHandler(IMessageHandler messageHandler, IBarHandler barHandler)
        {
            _messageHandler = messageHandler;
            _barHandler = barHandler;
        }

        public void RegisterHook(BarWindow window)
        {
            HwndSourceHook windowProcess = delegate (IntPtr handle, int message,
                IntPtr wParam, IntPtr lParam, ref bool isHandled)
            {
                if (message == WM_WINDOWPOSCHANGING && !window.IsBarResizing)
                {
                    WindowPos wp = Marshal.PtrToStructure<WindowPos>(lParam);
                    wp.Flags |= SWP_NOMOVE | SWP_NOSIZE;
                    Marshal.StructureToPtr(wp, lParam, false);
                }
                else if (message == WM_ACTIVATE)
                {
                    _messageHandler.SendMessage(window, AbMsg.ACTIVATE);
                }
                else if (message == WM_WINDOWPOSCHANGED)
                {
                    _messageHandler.SendMessage(window, AbMsg.WINDOWPOSCHANGED);
                }
                else if (message == window.MessageId)
                {
                    switch ((AbNotify)(int)wParam)
                    {
                        case AbNotify.POSCHANGED:
                            _barHandler.UpdateBar(window);
                            isHandled = true;
                            break;
                    }
                }
                return IntPtr.Zero;
            };
            HwndSource source = (HwndSource)PresentationSource.FromVisual(window);
            source.AddHook(windowProcess);
        }
    }
}
