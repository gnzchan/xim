using backend.Exceptions.Common;

namespace backend.Exceptions.LeaveRoom
{
    public class LeaveRoomException : CommonException
    {
        public LeaveRoomException(string msg) : base(msg)
        { }
    }
}