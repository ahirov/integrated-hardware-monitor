using IntegratedHardwareMonitor.Core.Entities.Enumerations;

namespace IntegratedHardwareMonitor.Core.Entities
{
    public sealed class ApplicationSetting
    {
        public Design Design { get; init; }
        public Position Position { get; init; }
        public string? DisplayId { get; init; }
        public List<HardwareComponent> Components { get; init; }

        public ApplicationSetting() {
            Design = Design.LIGHT;
            Position = Position.TOP;
            Components = new List<HardwareComponent>();
        }
    }
}
