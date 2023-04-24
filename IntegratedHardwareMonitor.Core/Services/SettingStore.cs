using System.Text.Json;

using IntegratedHardwareMonitor.Core.Entities;

namespace IntegratedHardwareMonitor.Core.Services
{
    public interface ISettingStore
    {
        ApplicationSetting Load();
        void Save(ApplicationSetting setting);
    }

    public sealed class SettingStore : ISettingStore
    {
        private readonly string _filePath = "setting.json";

        public ApplicationSetting Load()
        {
            ApplicationSetting result = new();
            if (File.Exists(_filePath))
            {
                string json = File.ReadAllText(_filePath);
                ApplicationSetting? setting = JsonSerializer.Deserialize<ApplicationSetting>(json);
                if (setting != null)
                {
                    result = setting;
                }
            }
            return result;
        }

        public void Save(ApplicationSetting setting)
        {
            string json = JsonSerializer.Serialize(setting);
            File.WriteAllText(_filePath, json);
        }
    }
}
