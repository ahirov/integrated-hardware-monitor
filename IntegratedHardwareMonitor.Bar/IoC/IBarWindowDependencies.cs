using AutoMapper;

using IntegratedHardwareMonitor.Bar.Services;
using IntegratedHardwareMonitor.Common.Services;

namespace IntegratedHardwareMonitor.Bar.IoC
{
    public interface IBarWindowDependencies
    {
        public IBarEventsHandler BarEventsHandler { get; }
        public IBarHandler BarHandler { get; }
        public ISourceHandler SourceHandler { get; }
        public IMessageHandler MessageHandler { get; }
        public IDisplayProvider DisplayProvider { get; }
        public ISettingHandler SettingProvider { get; }
        public IMapper Mapper { get; }
    }
}
