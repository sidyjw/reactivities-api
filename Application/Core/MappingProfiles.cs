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
            string currentUsername = null;
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
                    opt => opt.MapFrom(src => src.AppUser.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ForMember(dest => dest.FollowersCount, opt => opt.MapFrom(src => src.AppUser.Followers.Count))
                .ForMember(dest => dest.FollowingCount, opt => opt.MapFrom(src => src.AppUser.Followings.Count))
                .ForMember(dest => dest.Following,
                    opt => opt.MapFrom(src => src.AppUser.Followings.Any(x => x.Observer.UserName == currentUsername)));

            CreateMap<AppUser, Profiles.Profile>()
                .ForMember(dest => dest.Image,
                    opt => opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ForMember(dest => dest.FollowersCount, opt => opt.MapFrom(src => src.Followers.Count))
                .ForMember(dest => dest.FollowingCount, opt => opt.MapFrom(src => src.Followings.Count))
                .ForMember(dest => dest.Following, 
                    opt => opt.MapFrom(src => src.Followings.Any(x => x.Observer.UserName == currentUsername)));


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
