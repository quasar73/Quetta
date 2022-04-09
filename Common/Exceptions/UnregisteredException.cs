namespace Common.Exceptions
{
    public class UnregisteredException : Exception
    {
        public UnregisteredException(): base("The user is unregistered.") { }
    }
}
