namespace Common.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException() : base ("Entity Not found.") { }
        public EntityNotFoundException(string message) : base (message) { }
    }
}
