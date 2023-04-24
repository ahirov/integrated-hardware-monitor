using System.ComponentModel.DataAnnotations;

namespace IntegratedHardwareMonitor.Core.Entities
{
    public enum Position
    {
        [Display(Name = "Top")]
        TOP,
        [Display(Name = "Bottom")]
        BOTTOM
    }
}
