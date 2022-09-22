using Quetta.Data.Models;

namespace Quetta.Tests.Handlers.Commands
{
    public static class AcceptInviteTestUsers
    {
        public static User[] TestUsers = new User[]
        {
            new User()
            {
                Id = "00000000-0000-0000-0000-000000000000",
                UserName = "username1",
                FirstName = "First",
                LastName = "Lsat"
            },
            new User()
            {
                Id = "00000000-0000-0000-0000-000000000001",
                UserName = "username2",
                FirstName = "First",
                LastName = "Lsat"
            },
        };
    }
}
