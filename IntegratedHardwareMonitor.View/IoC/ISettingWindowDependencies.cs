using AutoMapper;

using IntegratedHardwareMonitor.Bar.Services;
using IntegratedHardwareMonitor.Common.Services;

namespace IntegratedHardwareMonitor.View.IoC
{
    public interface ISettingWindowDependencies
    {
        public IDisplayProvider DisplayProvider { get; }
        public ISettingHandler SettingHandler { get; }
        public IMapper Mapper { get; }
    }
}
