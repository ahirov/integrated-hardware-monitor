using AutoMapper;

using IntegratedHardwareMonitor.Bar.Services;
using IntegratedHardwareMonitor.Core.Services;

namespace IntegratedHardwareMonitor.IoC
{
    public interface ISettingWindowDependencies
    {
        public IDisplayProvider DisplayProvider { get; }
        public ISettingHandler SettingHandler { get; }
        public IMapper Mapper { get; }
    }
}
