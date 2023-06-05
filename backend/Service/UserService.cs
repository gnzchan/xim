using AutoMapper;
using backend.DTOs;
using backend.Models;

namespace backend.Service
{
    public class UserService
    {
        private readonly IMapper _mapper;

        public UserService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public UserDto GetUserDto(AppUser user)
        {
            var userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }
    }
}