using System.Text.Json;

using IntegratedHardwareMonitor.Core.Entities;

namespace IntegratedHardwareMonitor.Core.Services
{
    public interface ISettingStore
    {
        Setting Load();
        void Save(Setting setting);
    }

    public sealed class SettingStore : ISettingStore
    {
        private readonly string _filePath = "setting.json";

        public Setting Load()
        {
            Setting result = Setting.GetDefault();
            if (File.Exists(_filePath))
            {
                string json = File.ReadAllText(_filePath);
                Setting? setting = JsonSerializer.Deserialize<Setting>(json);
                if (setting != null)
                {
                    result = setting;
                }
            }
            return result;
        }

        public void Save(Setting setting)
        {
            string json = JsonSerializer.Serialize(setting);
            File.WriteAllText(_filePath, json);
        }
    }
}
