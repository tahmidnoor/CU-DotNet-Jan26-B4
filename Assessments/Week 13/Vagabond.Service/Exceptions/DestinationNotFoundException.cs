namespace Vagabond.Service.Exceptions
{
    public class DestinationNotFoundException : Exception
    {
        public DestinationNotFoundException(string message) : base(message) { }
    }
}