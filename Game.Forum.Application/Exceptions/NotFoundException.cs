namespace Game.Forum.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {

        }

        public NotFoundException() : base()
        {

        }
    }
}
