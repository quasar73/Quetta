using Quetta.Data.Models;
using System.Collections;

namespace Quetta.Tests.Handlers.Queries
{
    public class GetChatInfoTestCases : IEnumerable<object[]>
    {
        private const string PersonalChatId = "00000000-0000-0000-0000-000000000000";
        private const string GroupChatId = "00000000-0000-0000-0000-000000000001";
        private const string UserId = "00000000-0000-0000-0000-000000000000";
        private readonly User[] Users = new User[]
        {
            new User()
            {
                Id = UserId,
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
            },
        };

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new GetChatInfoTestCaseModel()
                {
                    ChatToAdd = new()
                    {
                        Id = PersonalChatId,
                        IsGroup = false,
                        Title = null,
                        Users = Users,
                    },
                    UsersToAdd = Users,
                    IncomingQuery = new()
                    {
                        UserId = UserId,
                        ChatId = PersonalChatId,
                    },
                    ExpectedChatInfo = new()
                    {
                        Title = "First2 Last2",
                        Members = 2,
                        IsGroup = false,
                    }
                }
            };
            yield return new object[]
            {
                new GetChatInfoTestCaseModel()
                {
                    ChatToAdd = new()
                    {
                        Id = GroupChatId,
                        IsGroup = true,
                        Title = "Test title",
                        Users = Users,
                    },
                    UsersToAdd = Users,
                    IncomingQuery = new()
                    {
                        UserId = UserId,
                        ChatId = GroupChatId,
                    },
                    ExpectedChatInfo = new()
                    {
                        Title = "Test title",
                        Members = 2,
                        IsGroup = true,
                    }
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
