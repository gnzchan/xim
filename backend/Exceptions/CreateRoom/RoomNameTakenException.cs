namespace backend.Exceptions
{
    public class RoomNameTakenException : Exception
    {
        public RoomNameTakenException() : base("The room name is already taken.")
        { }
    }
}