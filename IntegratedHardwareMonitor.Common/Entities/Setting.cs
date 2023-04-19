namespace IntegratedHardwareMonitor.Common.Entities
{
    public sealed class Setting
    {
        public Design Design { get; init; }
        public Position Position { get; init; }
        public string? DisplayId { get; init; }

        public static Setting GetDefault()
        {
            return new Setting()
            {
                Design = Design.LIGHT,
                Position = Position.TOP
            };
        }
    }
}
