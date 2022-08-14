using MediatR;

namespace  Quetta.Common.Models.Queries
{
    public class CheckOutUsernameQuery : IRequest<bool>
    {
        public string Username { get; set; }

        public CheckOutUsernameQuery(string username)
        {
            Username = username;
        }
    }
}
