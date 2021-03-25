﻿using Application.Activities;
using AutoMapper;
using Domain;
using System.Linq;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Activity, Activity>();
            CreateMap<Activity, ActivityDTO>()
                .ForMember(
                    dest => dest.HostUsername,
                    opt => opt
                        .MapFrom(src => src.Attendees
                            .FirstOrDefault(x => x.IsHost)
                                .AppUser.UserName)
                );
            CreateMap<ActivityAttendee, Profiles.Profile>()
                .ForMember(dest => dest.DisplayName,
                    opt => opt.MapFrom(src => src.AppUser.DisplayName))
                .ForMember(dest => dest.Username,
                    opt => opt.MapFrom(src => src.AppUser.UserName))
                .ForMember(dest => dest.Bio,
                    opt => opt.MapFrom(src => src.AppUser.Bio));
        }
    }
}
