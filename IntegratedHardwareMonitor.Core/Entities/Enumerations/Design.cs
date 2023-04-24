using System.ComponentModel.DataAnnotations;

namespace IntegratedHardwareMonitor.Core.Entities.Enumerations
{
    public enum Design
    {
        [Display(Name = "Light")]
        LIGHT,
        [Display(Name = "Dark")]
        DARK
    }
}
