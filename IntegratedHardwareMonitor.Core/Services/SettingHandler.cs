using IntegratedHardwareMonitor.Core.Entities;

namespace IntegratedHardwareMonitor.Core.Services
{
    public interface ISettingHandler
    {
        Setting Setting { get; }
        List<Item<Design>> GetThemes();
        List<Item<Position>> GetPositions();
        void Save(Setting setting);
    }

    public sealed class SettingHandler : ISettingHandler
    {
        private readonly ISettingStore _store;
        public Setting Setting { get; }

        public SettingHandler(ISettingStore store)
        {
            _store = store;
            Setting = _store.Load();
        }

        public List<Item<Design>> GetThemes()
        {
            return Enum
                .GetValues<Design>()
                .Select(item => new Item<Design>(item))
                .ToList();
        }

        public List<Item<Position>> GetPositions()
        {
            return Enum
                .GetValues<Position>()
                .Select(item => new Item<Position>(item))
                .ToList();
        }

        public void Save(Setting setting)
        {
            _store.Save(setting);
        }
    }
}
