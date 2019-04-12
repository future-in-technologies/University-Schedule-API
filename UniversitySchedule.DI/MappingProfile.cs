using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using UniversitySchedule.Core.Entites;
using UniversitySchedule.Core.Models.ViewModel;

namespace UniversitySchedule.DI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserViewModel>();
            CreateMap<RegistrationViewModel, User>();
        }
    }
}
