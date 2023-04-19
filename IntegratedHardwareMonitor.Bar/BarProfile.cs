using AutoMapper;
using AutoMapper.Extensions.EnumMapping;

using IntegratedHardwareMonitor.Bar.Controls;
using IntegratedHardwareMonitor.Common.Entities;

namespace IntegratedHardwareMonitor.Bar
{
    internal sealed class BarProfile : Profile
    {
        public BarProfile()
        {
            CreateMap<Position, BarPosition>()
                .ConvertUsingEnumMapping(options => options.MapByName())
                .ReverseMap();
        }
    }
}
