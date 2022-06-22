namespace Common.Exceptions
{
    public class InvalidTokenException : Exception
    {
        public InvalidTokenException() : base("The token is invalid.") { }
    }
}
