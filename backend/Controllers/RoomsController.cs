using System.Linq;
using backend.DTOs;
using backend.Exceptions;
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
            var rooms = await _service.GetRooms();
            return Ok(rooms);
        }

        [HttpGet("{id}", Name = "GetRoom")]
        public async Task<ActionResult<ReadRoomDto>> GetRoom(Guid id)
        {
            var room = await _service.GetRoom(id);

            if (room == null)
            {
                return NotFound();
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
                    return BadRequest();
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
                _service.DeleteRoom(id);
                var isSuccess = await _service.SaveChanges();

                if (!isSuccess)
                {
                    return BadRequest();
                }

                return NoContent();
            }
            catch (DeletingNonExistingRoomException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}