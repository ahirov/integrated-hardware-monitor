using AutoMapper;
using AutoMapper.Extensions.EnumMapping;

using IntegratedHardwareMonitor.Bar.Controls;
using IntegratedHardwareMonitor.Core.Entities;

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
