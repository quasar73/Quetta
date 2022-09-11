using Quetta.Data.Models;

namespace Quetta.Tests.Handlers.Data
{
    public static class ChatsTestData
    {
        public static List<Chat> TestChats => new List<Chat>
        {
            new Chat()
            {
                Id = "00000000-0000-0000-0000-000000000000",
                Title = null,
                IsGroup = false,
                Users = new List<User>(),
                Messages = new List<Message>(),
            },
            new Chat()
            {
                Id = "00000000-0000-0000-0000-000000000001",
                Title = "Test Group",
                IsGroup = true,
                Users = new List<User>(),
                Messages = new List<Message>(),
            }
        };

        public static List<Message> TestMessages => new List<Message>
        {
            new Message()
            {
                Id = Guid.NewGuid().ToString(),
                Text = "This is last message 1",
                Date = new DateTime(2022, 01, 01, 01, 01, 01),
            },
            new Message()
            {
                Id = Guid.NewGuid().ToString(),
                Text = "This is last message 2",
                Date = new DateTime(2022, 01, 01, 01, 01, 01),
            },
        };

        public static List<User> TestUsers => new List<User>
        {
            new User()
            {
                Id = "00000000-0000-0000-0000-000000000000",
                UserName = "username1",
                FirstName = "First",
                LastName = "Last",
                Email = "username@email.com"
            },
            new User()
            {
                Id = "10000000-0000-0000-0000-000000000000",
                UserName = "username2",
                FirstName = "First",
                LastName = "Last",
                Email = "username2@mail.com"
            }
        };
    }
}
