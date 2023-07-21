using backend.DTOs;
using backend.Models;

namespace backend.Hubs.Clients
{
    public interface IGroupClient
    {
        Task SendMessage(string message);
        Task UpdateGroups(List<GroupDto> groups);
    }
}