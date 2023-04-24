using System.ComponentModel.DataAnnotations;

namespace IntegratedHardwareMonitor.Core.Entities.Enumerations
{
    public enum Position
    {
        [Display(Name = "Top")]
        TOP,
        [Display(Name = "Bottom")]
        BOTTOM
    }
}
