using System.ComponentModel.DataAnnotations;

namespace IntegratedHardwareMonitor.Common.Entities
{
    public enum Design
    {
        [Display(Name = "Light")]
        LIGHT,
        [Display(Name = "Dark")]
        DARK
    }
}
