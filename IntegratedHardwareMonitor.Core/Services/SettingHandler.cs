using IntegratedHardwareMonitor.Core.Entities;
using IntegratedHardwareMonitor.Core.Entities.Enumerations;

namespace IntegratedHardwareMonitor.Core.Services
{
    public interface ISettingHandler
    {
        ApplicationSetting Setting { get; }
        List<EnumWrapper<Design>> GetThemes();
        List<EnumWrapper<Position>> GetPositions();
        void Save(ApplicationSetting setting);
    }

    public sealed class SettingHandler : ISettingHandler
    {
        private readonly ISettingStore _store;
        public ApplicationSetting Setting { get; }

        public SettingHandler(ISettingStore store)
        {
            _store = store;
            Setting = _store.Load();
        }

        public List<EnumWrapper<Design>> GetThemes()
        {
            return Enum
                .GetValues<Design>()
                .Select(item => new EnumWrapper<Design>(item))
                .ToList();
        }

        public List<EnumWrapper<Position>> GetPositions()
        {
            return Enum
                .GetValues<Position>()
                .Select(item => new EnumWrapper<Position>(item))
                .ToList();
        }

        public void Save(ApplicationSetting setting)
        {
            _store.Save(setting);
        }
    }
}
