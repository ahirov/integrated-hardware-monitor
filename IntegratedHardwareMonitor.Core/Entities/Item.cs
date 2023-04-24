using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace IntegratedHardwareMonitor.Core.Entities
{
    public sealed class Item<T> where T : struct, IConvertible
    {
        public T Value { get; }
        public string Name { get; } = string.Empty;

        public Item(T value)
        {
            Value = value;

            string? fullName = value.ToString();
            if (fullName != null)
            {
                string? name = value
                    .GetType()
                    .GetMember(fullName)
                    .First()
                    .GetCustomAttribute<DisplayAttribute>()?.Name;
                if (name != null)
                {
                    Name = name;
                }
            }
        }
    }
}
