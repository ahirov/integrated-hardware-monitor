using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;

namespace IntegratedHardwareMonitor
{
    public interface IExceptionHandler
    {
        public void DisplayException(object sender, DispatcherUnhandledExceptionEventArgs args);
    }

    public sealed class ExceptionHandler : IExceptionHandler
    {
        public void DisplayException(object sender, DispatcherUnhandledExceptionEventArgs args)
        {
            ShowErrorMessage(args.Exception);
            args.Handled = true;
        }

        private void ShowErrorMessage(Exception ex)
        {
            if (Debugger.IsAttached)
            {
                if (ex.InnerException != null)
                {
                    ShowErrorMessage(ex.InnerException);
                }
                string caption = ex.GetType().Name;
                string message = $"{ex.Source}\r\nError: {ex.Message}\r\n{ex.StackTrace}";
                ShowMessageBox(caption, message);
            }
            else
            {
                ShowMessageBox("Error occurred!!!",
                    "Something happened!\r\nContact your administrator.");
            }
        }

        private static void ShowMessageBox(string caption, string message)
        {
            _ = MessageBox.Show(
                message,
                caption,
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
}
