using System;

using AutoMapper;

using IntegratedHardwareMonitor.Core.Entities.Enumerations;

using Wpf.Ui.Appearance;

namespace IntegratedHardwareMonitor
{
    internal sealed class ViewProfile : Profile
    {
        public ViewProfile()
        {
            CreateMap<Design, ThemeType>().ConvertUsing(design => GetEnum(design));
            CreateMap<ThemeType, Design>().ConvertUsing(theme => GetEnum(theme));
        }

        private static ThemeType GetEnum(Design design)
        {
            return (ThemeType)Enum.Parse(typeof(ThemeType), design.ToString(), true);
        }

        private static Design GetEnum(ThemeType theme)
        {
            return (Design)Enum.Parse(typeof(Design), theme.ToString(), true);
        }
    }
}
