using AutoMapper;
using backend.DTOs;
using backend.Models;

namespace backend.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<AppUser, UserDto>();
            CreateMap<RoomAttendee, UserDto>()
                .ForMember(u => u.FirstName, opt => opt.MapFrom(src => src.AppUser.FirstName))
                .ForMember(u => u.LastName, opt => opt.MapFrom(src => src.AppUser.LastName))
                .ForMember(u => u.Username, opt => opt.MapFrom(src => src.AppUser.UserName))
                .ForMember(u => u.Email, opt => opt.MapFrom(src => src.AppUser.Email));
            CreateMap<GroupAttendee, UserDto>()
                .ForMember(u => u.FirstName, opt => opt.MapFrom(src => src.AppUser.FirstName))
                .ForMember(u => u.LastName, opt => opt.MapFrom(src => src.AppUser.LastName))
                .ForMember(u => u.Username, opt => opt.MapFrom(src => src.AppUser.UserName))
                .ForMember(u => u.Email, opt => opt.MapFrom(src => src.AppUser.Email))
                .ReverseMap();
        }
    }
}