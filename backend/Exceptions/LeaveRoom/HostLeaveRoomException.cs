namespace backend.Exceptions.LeaveRoom
{
    public class HostLeaveRoomException : LeaveRoomException
    {
        public HostLeaveRoomException() :
            base("You are the host of this room. You can't leave your attendees hanging."
            + " Please delete the room instead if you really want to.")
        { }
    }
}