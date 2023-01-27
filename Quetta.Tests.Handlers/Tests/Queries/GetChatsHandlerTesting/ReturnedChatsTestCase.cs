using Quetta.Common.Enums;
using Quetta.Common.Models.Queries;
using Quetta.Common.Models.Responses;
using Quetta.Data.Models;
using System.Collections;

namespace Quetta.Tests.Handlers.Queries
{
    public class ReturnedChatsTestCase : IEnumerable<object[]>
    {
        private const string ExistingUserId = "00000000-0000-0000-0000-000000000001";
        private readonly User[] Users = new User[]
        {
            new User()
            {
                Id = ExistingUserId,
                UserName = "username1",
                FirstName = "First1",
                LastName = "Last1"
            },
            new User()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "username2",
                FirstName = "First2",
                LastName = "Last2"
            }
        };
        private readonly Message[] Messages = new Message[]
        {
            new Message()
            {
                Id = Guid.NewGuid().ToString(),
                Text = "Test text",
                UserId = ExistingUserId,
            },
        };

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new GetChatsTestCaseModel()
                {
                    UsersToAdd = Users,
                    ChatsToAdd = new Chat[]
                    {
                        new Chat()
                        {
                            Id = "00000000-0000-0000-0000-000000000001",
                            Users = Users,
                            Messages = Messages,
                            IsGroup = false,
                        },
                        new Chat()
                        {
                            Id = "00000000-0000-0000-0000-000000000002",
                            Users = Users,
                            Messages = new List<Message>(),
                            IsGroup = true,
                            Title = "Test title"
                        },
                        new Chat()
                        {
                            Id = "00000000-0000-0000-0000-000000000003",
                            Users = Array.Empty<User>(),
                            Messages = new List<Message>(),
                            IsGroup = true,
                            Title = "Test title"
                        }
                    },
                    MessagesToAdd = Array.Empty<Message>(),
                    IncomingQuery = new GetChatsQuery(ExistingUserId),
                    ExpectedReturnedChats = new ChatItemResponse[]
                    {
                        new ChatItemResponse
                        {
                            ChatType = ChatType.PersonalChat,
                            Id = "00000000-0000-0000-0000-000000000001",
                            LastMessage = "Test text",
                            Title = "Decrypted message."
                        },
                        new ChatItemResponse
                        {
                            ChatType = ChatType.GroupChat,
                            Id = "00000000-0000-0000-0000-000000000002",
                            LastMessage = null,
                            Title = "Decrypted message."
                        }
                    },
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
