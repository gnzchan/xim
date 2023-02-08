using AutoMapper;
using backend.DTOs;
using backend.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace backend.Profiles
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<Room, ReadRoomDto>();
            CreateMap<CreateRoomDto, Room>()
                .ForMember(room => room.RoomId, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(room => room.RoomCode, opt => opt.MapFrom(src => Guid.NewGuid().ToString().Substring(0, 6)));
        }
    }
}