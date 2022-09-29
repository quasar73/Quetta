using Quetta.Common.Models.Commands;
using Quetta.Data.Models;
using System.Collections;

namespace Quetta.Tests.Handlers.Commands
{
    public class DeleteMessagesTestCase : IEnumerable<object[]>
    {
        private static readonly List<User> TestUsers = new()
        {
            new User()
            {
                Id = "00000000-0000-0000-0000-000000000000",
                UserName = "username1",
                FirstName = "First",
                LastName = "Last",
            },
            new User()
            {
                Id = "00000000-0000-0000-0000-000000000001",
                UserName = "username2",
                FirstName = "First",
                LastName = "Last",
            },
        };
        private readonly Chat AllowedChat = new()
        {
            Id = "00000000-0000-0000-0000-000000000000",
            IsGroup = false,
            Users = TestUsers,
        };
        private readonly Chat NotAllowedChat = new()
        {
            Id = "00000000-0000-0000-0000-000000000001",
            IsGroup = false,
            Users = new List<User>(),
        };

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new DeleteMessagesTestCaseModel()
                {
                    MessagesToAdd = new Message[]
                    {
                        new Message()
                        {
                            Id = "00000000-0000-0000-0000-000000000000",
                            Text = "This is test text 1",
                            Date = DateTime.Now,
                            Chat = AllowedChat,
                            UserId = TestUsers.First().Id,
                        },
                        new Message()
                        {
                            Id = "00000000-0000-0000-0000-000000000001",
                            Text = "This is test text 2",
                            Date = DateTime.Now,
                            Chat = AllowedChat,
                            UserId = TestUsers.First().Id,
                        },
                        new Message()
                        {
                            Id = "00000000-0000-0000-0000-000000000002",
                            Text = "This is test text 3",
                            Date = DateTime.Now,
                            Chat = AllowedChat,
                            UserId = TestUsers.First().Id,
                        },
                        new Message()
                        {
                            Id = "00000000-0000-0000-0000-000000000003",
                            Text = "This is test text 4",
                            Date = DateTime.Now,
                            Chat = NotAllowedChat,
                            UserId = TestUsers.First().Id,
                        },
                    },
                    ChatsToAdd = new Chat[]
                    {
                        AllowedChat,
                        NotAllowedChat,
                    },
                    UsersToAdd = TestUsers.ToArray(),
                    IncomingCommand = new DeleteMessagesCommand()
                    {
                        MessageIds = new()
                        {
                            "00000000-0000-0000-0000-000000000000",
                            "00000000-0000-0000-0000-000000000001",
                        },
                        UserId = "00000000-0000-0000-0000-000000000000",
                    },
                    ExpectedAmount = 2,
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
