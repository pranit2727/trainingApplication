using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TrainingContextLayer;
using ViewModels;

namespace Services.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TrainingVM, Schedule>();
            CreateMap<TrainingVM, Training>().ForMember(src => src.CreatedAt, opt => opt.MapFrom(src=>new { DateTime.Now }));
            CreateMap<TrainingVM, Training>().ForMember(src => src.UpdatedAt, opt => opt.MapFrom(src => new { DateTime.Now }));
        }
    }
}
