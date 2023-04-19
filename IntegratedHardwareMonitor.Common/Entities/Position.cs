using System.ComponentModel.DataAnnotations;

namespace IntegratedHardwareMonitor.Common.Entities
{
    public enum Position
    {
        [Display(Name = "Top")]
        TOP,
        [Display(Name = "Bottom")]
        BOTTOM
    }
}
