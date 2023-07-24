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
    public class GroupsController : ControllerBase
    {
        private readonly RoomService _roomService;
        private readonly GroupService _groupService;
        private readonly UserManager<AppUser> _userManager;

        public GroupsController(RoomService roomService, GroupService groupService, UserManager<AppUser> userManager)
        {
            _roomService = roomService;
            _groupService = groupService;
            _userManager = userManager;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Group>>> GetGroups(Guid id)
        {
            var user = await _userManager.GetUserAsync(User);
            var room = await _roomService.GetRoom(id);

            if (!room.Attendees.Any(a => a.AppUser.UserName == user.UserName))
            {
                return Unauthorized();
            }

            var groups = await _groupService.GetGroups(id);

            return Ok(groups);
        }

        [HttpGet("{id}/{numberOfGroups}")]
        public async Task<ActionResult<List<Group>>> CreateGroups(Guid id, int numberOfGroups)
        {
            var user = await _userManager.GetUserAsync(User);
            var room = await _roomService.GetRoom(id);

            if (room.HostUsername != user.UserName)
            {
                return Unauthorized();
            }

            var groups = await _groupService.CreateGroups(room, numberOfGroups);
            var isSuccess = await _groupService.SaveChanges();

            if (!isSuccess)
            {
                return BadRequest(new SaveToServerException().Message);
            }

            return Ok(groups);
        }

    }
}