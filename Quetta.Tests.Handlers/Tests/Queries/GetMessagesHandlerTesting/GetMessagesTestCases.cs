using Quetta.Common.Models.Responses;
using Quetta.Data.Models;
using System.Collections;

namespace Quetta.Tests.Handlers.Queries
{
    public class GetMessagesTestCases : IEnumerable<object[]>
    {
        private readonly User[] Users = new User[]
        {
            new User()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "username1",
                FirstName = "First",
                LastName = "Last"
            },
            new User()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "username2",
                FirstName = "First",
                LastName = "Last"
            },
            new User()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "username3",
                FirstName = "First",
                LastName = "Last"
            },
        };

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new GetMessagesTestCaseModel()
                {
                    UsersToAdd = Users,
                    MessagesToAdd = new Message[]
                    {
                        new Message()
                        {
                            Id = "00000000-0000-0000-0000-000000000000",
                            Text = "Test message one",
                            ChatId = "00000000-0000-0000-0000-000000000000",
                            User = Users[0],
                            Date = new DateTime(2022, 01, 01),
                        },
                        new Message()
                        {
                            Id = "00000000-0000-0000-0000-000000000001",
                            Text = "Test message two",
                            ChatId = "00000000-0000-0000-0000-000000000000",
                            User = Users[1],
                            Date = new DateTime(2022, 01, 02),
                        },
                        new Message()
                        {
                            Id = "00000000-0000-0000-0000-000000000002",
                            Text = "Test message three",
                            ChatId = "00000000-0000-0000-0000-000000000000",
                            User = Users[2],
                            Date = new DateTime(2022, 01, 03),
                        },
                        new Message()
                        {
                            Id = "00000000-0000-0000-0000-000000000003",
                            Text = "Test message four",
                            ChatId = "00000000-0000-0000-0000-000000000001",
                            User = Users[0],
                            Date = new DateTime(2022, 01, 01),
                        },
                    },
                    IncomingQuery = new("00000000-0000-0000-0000-000000000000"),
                    ExpectedReturnedMessages = new MessageResponse[]
                    {
                        new MessageResponse
                        {
                            Text = "Test message one",
                            Username = "username1",
                            Date = new DateTime(2022, 01, 01),
                        },
                        new MessageResponse
                        {
                            Text = "Test message two",
                            Username = "username2",
                            Date = new DateTime(2022, 01, 02),
                        },
                        new MessageResponse
                        {
                            Text = "Test message three",
                            Username = "username3",
                            Date = new DateTime(2022, 01, 03),
                        },
                    }
                }
            };
            yield return new object[]
            {
                new GetMessagesTestCaseModel()
                {
                    UsersToAdd = Array.Empty<User>(),
                    MessagesToAdd = Array.Empty<Message>(),
                    IncomingQuery = new("00000000-0000-0000-0000-000000000000"),
                    ExpectedReturnedMessages = Array.Empty<MessageResponse>(),
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
