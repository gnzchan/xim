using backend.DTOs;
using backend.Exceptions;
using backend.Exceptions.Common;
using backend.Models;
using backend.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly RoomService _service;
        private readonly UserManager<AppUser> _userManager;

        public RoomsController(RoomService service, UserManager<AppUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<ReadRoomDto>>> GetAllRooms()
        {
            var user = await _userManager.GetUserAsync(User);
            var rooms = await _service.GetRoomsDto();
            return Ok(rooms);
        }

        [HttpGet("own")]
        public async Task<ActionResult<List<ReadRoomDto>>> GetMyRooms()
        {
            var user = await _userManager.GetUserAsync(User);
            var rooms = await _service.GetMyRooms(user);
            return Ok(rooms);
        }

        [HttpGet("{id}", Name = "GetRoom")]
        public async Task<ActionResult<ReadRoomDto>> GetRoom(Guid id)
        {
            var room = await _service.GetRoomDto(id);

            if (room == null)
            {
                return NotFound(new RoomNotFoundException().Message);
            }

            return Ok(room);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom(CreateRoomDto createRoomDto)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var room = await _service.CreateRoom(createRoomDto, user);
                var isSuccess = await _service.SaveChanges();

                if (!isSuccess)
                {
                    return BadRequest(new SaveToServerException().Message);
                }

                return CreatedAtRoute("GetRoom", new { id = room.RoomId }, room);
            }
            catch (RoomNameTakenException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(Guid id)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                await _service.DeleteRoom(id, user);
                var isSuccess = await _service.SaveChanges();

                if (!isSuccess)
                {
                    return BadRequest(new SaveToServerException().Message);
                }

                return NoContent();
            }
            catch (DeleteRoomException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{code}/join")]
        public async Task<IActionResult> JoinRoom(string code)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var room = await _service.JoinRoom(code, user);
                var isSuccess = await _service.SaveChanges();

                if (!isSuccess)
                {
                    return BadRequest(new SaveToServerException().Message);
                }

                return Ok();
            }
            catch (CommonException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{code}/leave")]
        public async Task<IActionResult> LeaveRoom(string code)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                await _service.LeaveRoom(code, user);
                var isSuccess = await _service.SaveChanges();

                if (!isSuccess)
                {
                    return BadRequest(new SaveToServerException().Message);
                }

                return NoContent();
            }
            catch (CommonException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}