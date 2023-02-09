using backend.Exceptions.Common;

namespace backend.Exceptions.JoinRoom
{
    public class AlreadyJoinedRoomException : CommonException
    {
        public AlreadyJoinedRoomException() : base("You are already joining this room.")
        { }
    }
}