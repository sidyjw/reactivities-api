using Application.Activities;
using Application.Comments;
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
            CreateMap<ActivityAttendee, AttendeeDTO>()
                .ForMember(dest => dest.DisplayName,
                    opt => opt.MapFrom(src => src.AppUser.DisplayName))
                .ForMember(dest => dest.UserName,
                    opt => opt.MapFrom(src => src.AppUser.UserName))
                .ForMember(dest => dest.Bio,
                    opt => opt.MapFrom(src => src.AppUser.Bio))
                .ForMember(dest => dest.Image,
                    opt => opt.MapFrom(src => src.AppUser.Photos.FirstOrDefault(p => p.IsMain).Url));

            CreateMap<AppUser, Profiles.Profile>()
                .ForMember(dest => dest.Image,
                    opt => opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url));
            CreateMap<Profiles.Profile, AppUser>();
            CreateMap<Comment, CommentDTO>()
                .ForMember(dest => dest.DisplayName,
                    opt => opt.MapFrom(src => src.Author.DisplayName))
                .ForMember(dest => dest.Username,
                    opt => opt.MapFrom(src => src.Author.UserName))
                .ForMember(dest => dest.Image,
                    opt => opt.MapFrom(src => src.Author.Photos.FirstOrDefault(p => p.IsMain).Url));
        }
    }
}
