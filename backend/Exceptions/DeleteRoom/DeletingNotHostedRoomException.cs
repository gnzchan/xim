namespace backend.Exceptions
{
    public class DeletingNotHostedRoomException : DeleteRoomException
    {
        public DeletingNotHostedRoomException() : base("You can't delete a room you did not created.")
        { }
    }
}