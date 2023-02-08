namespace backend.Exceptions
{
    public class DeletingNonExistingRoomException : Exception
    {
        public DeletingNonExistingRoomException() : base("The room you are trying to delete does not exist.")
        { }
    }
}