using System;
using System.Text.RegularExpressions;
using System.Windows;

namespace IntegratedHardwareMonitor.Bar.Entities
{
    public sealed class DisplayInfo : IEquatable<DisplayInfo>
    {
        public bool IsPrimary { get; }
        public string Id { get; }
        public string Name { get; }
        public Rect WorkArea { get; }
        public Rect Viewport { get; }

        internal DisplayInfo(MonitorInfoEx mex)
        {
            IsPrimary = mex.Flags.HasFlag(MonitorInfoF.PRIMARY);
            Id = mex.DeviceName;
            Name = Regex.Match(mex.DeviceName, @"\d+").Value;
            WorkArea = (Rect)mex.WorkArea;
            Viewport = (Rect)mex.Monitor;
        }

        public static bool operator ==(DisplayInfo display1, DisplayInfo display2)
        {
            return display1?.Equals(display2) ?? false;
        }

        public static bool operator !=(DisplayInfo display1, DisplayInfo display2)
        {
            return !(display1 == display2);
        }

        public override string ToString()
        {
            return Id;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as DisplayInfo);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public bool Equals(DisplayInfo display)
        {
            return Id == display?.Id;
        }
    }
}
