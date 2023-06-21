using backend.Models;

namespace backend.Hubs.Clients
{
    public interface IGroupClient
    {
        Task ReceiveGroup(Group group);
    }
}