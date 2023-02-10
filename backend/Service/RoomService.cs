using AutoMapper;
using backend.Data;
using backend.DTOs;
using backend.Exceptions;
using backend.Exceptions.Common;
using backend.Exceptions.JoinRoom;
using backend.Exceptions.LeaveRoom;
using backend.Models;

namespace backend.Service
{
    public class RoomService
    {
        private readonly RoomRepository _repository;
        private readonly IMapper _mapper;

        public RoomService(RoomRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ReadRoomDto>> GetRooms()
        {
            var rooms = await _repository.GetRoomsDto();

            return rooms;
        }

        public async Task<ReadRoomDto> GetRoom(Guid id)
        {
            var room = await _repository.GetRoomDto(id);

            return room;
        }

        public async Task<ReadRoomDto> CreateRoom(CreateRoomDto createRoomDto, AppUser user)
        {
            var similarRoomName = await _repository.GetRoomByName(createRoomDto.RoomName);

            if (similarRoomName != null)
            {
                throw new RoomNameTakenException();
            }

            var room = _mapper.Map<Room>(createRoomDto);

            var roomAttendee = new RoomAttendee
            {
                AppUserId = user.Id,
                Room = room,
            };

            room.HostUsername = user.UserName;
            room.Attendees.Add(roomAttendee);

            await _repository.CreateRoom(room);

            return _mapper.Map<ReadRoomDto>(room);
        }

        public async Task DeleteRoom(Guid id, AppUser user)
        {
            var readRoomDto = await _repository.GetRoomDto(id);

            if (readRoomDto == null)
            {
                throw new DeletingNonExistingRoomException();
            }

            if (readRoomDto.HostUsername != user.UserName)
            {
                throw new DeletingNotHostedRoomException();
            }

            await _repository.DeleteRoom(readRoomDto.RoomId);
        }

        public async Task<ReadRoomDto> JoinRoom(string code, AppUser user)
        {
            var room = await _repository.GetRoom(code);

            if (room == null)
            {
                throw new RoomNotFoundException();
            }

            if (room.HostUsername == user.UserName || room.Attendees.Any(ra => ra.AppUser.UserName == user.UserName))
            {
                throw new AlreadyJoinedRoomException();
            }

            var roomAttendee = new RoomAttendee
            {
                AppUserId = user.Id,
                Room = room,
            };

            room.Attendees.Add(roomAttendee);

            return _mapper.Map<ReadRoomDto>(room);
        }

        public async Task LeaveRoom(string code, AppUser user)
        {
            var room = await _repository.GetRoom(code);

            if (room == null)
            {
                throw new RoomNotFoundException();
            }

            if (room.HostUsername == user.UserName)
            {
                throw new HostLeaveRoomException();
            }

            var roomAttendee = room.Attendees.FirstOrDefault(ra => ra.AppUser.UserName == user.UserName);

            if (roomAttendee == null)
            {
                throw new NotJoinedLeaveRoomException();
            }

            room.Attendees.Remove(roomAttendee);
        }

        public async Task<bool> SaveChanges()
        {
            return await _repository.SaveChanges();
        }
    }
}