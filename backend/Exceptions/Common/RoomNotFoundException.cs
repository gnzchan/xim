namespace backend.Exceptions.Common
{
    public class RoomNotFoundException : CommonException
    {
        public RoomNotFoundException() : base("The room you are trying to access can't be found.")
        { }
    }
}