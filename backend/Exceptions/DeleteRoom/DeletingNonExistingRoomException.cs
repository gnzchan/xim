namespace backend.Exceptions
{
    public class DeletingNonExistingRoomException : DeleteRoomException
    {
        public DeletingNonExistingRoomException() : base("The room you are trying to delete does not exist.")
        { }
    }
}