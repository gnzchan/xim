namespace backend.Exceptions.Common
{
    public class SaveToServerException : CommonException
    {
        public SaveToServerException() : base("The data can't be saved on the server. Please contact XIM admin.")
        { }
    }
}