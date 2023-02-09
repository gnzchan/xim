namespace backend.Exceptions.LeaveRoom
{
    public class NotJoinedLeaveRoomException : LeaveRoomException
    {
        public NotJoinedLeaveRoomException() :
            base("There's no point in leaving. You haven't joined this room at the first place.")
        { }
    }
}