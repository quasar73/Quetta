using Quetta.Common.Exceptions;
using Quetta.Common.Models.Commands;
using Quetta.Data.Models;
using System.Collections;

namespace Quetta.Tests.Handlers.Commands
{
    public class DeleteMessagesExcetpionTestCase : IEnumerable<object[]>
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
                new DeleteMessagesExceptionCaseModel()
                {
                    MessagesToAdd = Array.Empty<Message>(),
                    ChatsToAdd = Array.Empty<Chat>(),
                    UsersToAdd = TestUsers.ToArray(),
                    IncomingCommand = new DeleteMessagesCommand()
                    {
                        MessageIds = new()
                        {
                            "00000000-0000-0000-0000-000000000000",
                        },
                        UserId = "00000000-0000-0000-0000-000000000000",
                    },
                    ExceptionType = typeof(EntityNotFoundException),
                    ExceptionMessage = "Entity not found."
                },
            };
            yield return new object[]
            {
                new DeleteMessagesExceptionCaseModel()
                {
                    MessagesToAdd = new Message[]
                    {
                        new Message()
                        {
                            Id = "00000000-0000-0000-0000-000000000000",
                            Text = "This is test text 1",
                            Date = DateTime.Now,
                            Chat = NotAllowedChat,
                            UserId = TestUsers.Last().Id,
                        },
                    },
                    ChatsToAdd = new Chat[]
                    {
                        NotAllowedChat,
                    },
                    UsersToAdd = TestUsers.ToArray(),
                    IncomingCommand = new DeleteMessagesCommand()
                    {
                        MessageIds = new()
                        {
                            "00000000-0000-0000-0000-000000000000",
                        },
                        UserId = "00000000-0000-0000-0000-000000000000",
                    },
                    ExceptionType = typeof(AccessDeniedException),
                    ExceptionMessage = "Access denied."
                },
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
