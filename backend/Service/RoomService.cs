using AutoMapper;
using backend.Data;
using backend.DTOs;
using backend.Exceptions;
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
            var rooms = _mapper.Map<List<ReadRoomDto>>(await _repository.GetRooms());

            return rooms;
        }

        public async Task<ReadRoomDto> GetRoom(Guid id)
        {
            var room = _mapper.Map<ReadRoomDto>(await _repository.GetRoomById(id));

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
                IsHost = true
            };

            room.Attendees.Add(roomAttendee);

            _repository.CreateRoom(room);

            return _mapper.Map<ReadRoomDto>(room);
        }

        public async void DeleteRoom(Guid id)
        {
            var room = await _repository.GetRoomById(id);

            if (room == null)
            {
                throw new DeletingNonExistingRoomException();
            }

            _repository.DeleteRoom(room);
        }

        public async Task<bool> SaveChanges()
        {
            return await _repository.SaveChanges() > 0;
        }
    }
}