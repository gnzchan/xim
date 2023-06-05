using AutoMapper;
using backend.DTOs;
using backend.Models;

namespace backend.Profiles
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<Room, ReadRoomDto>()
                .ReverseMap();

            CreateMap<CreateRoomDto, Room>()
                .ForMember(room => room.RoomId, opt => opt
                    .MapFrom(src => Guid.NewGuid()))
                .ForMember(room => room.RoomCode, opt => opt
                    .MapFrom(src => Guid.NewGuid().ToString().Substring(0, 6).ToUpper()))
                .ForMember(room => room.RoomName, opt => opt
                    .MapFrom(src => src.RoomName.ToUpper()));
        }
    }
}