namespace Quetta.Common.Exceptions
{
    public class UserAlreadyInvitedException : Exception
    {
        public UserAlreadyInvitedException() : base("User already invited.") { }
    }
}
