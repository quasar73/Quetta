using Quetta.Common.Exceptions;
using Quetta.Data.Models;
using System.Collections;

namespace Quetta.Tests.Handlers.Queries
{
    public class GetChatInfoExceptionTestCases : IEnumerable<object[]>
    {
        private const string ExistingChatId = "00000000-0000-0000-0000-000000000000";    
        private const string NonexistentChatId = "00000000-0000-0000-0000-000000000001";    
        private const string PrivateChatId = "00000000-0000-0000-0000-000000000002";    
        private const string UserId = "00000000-0000-0000-0000-000000000000";
        private const string StrangerUserId = "00000000-0000-0000-0000-000000000001";
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
                new GetChatInfoTestExceptionModel()
                {
                    ChatToAdd = new()
                    {
                        Id = ExistingChatId,
                        IsGroup = false,
                        Title = null,
                        Users = Users,
                    },
                    UsersToAdd = Users,
                    IncomingQuery = new()
                    {
                        UserId = UserId,
                        ChatId = NonexistentChatId,
                    },
                    ExceptionType = typeof(EntityNotFoundException),
                    ExceptionMessage = "Entity not found.",
                }
            };
            yield return new object[]
            {
                new GetChatInfoTestExceptionModel()
                {
                    ChatToAdd = new()
                    {
                        Id = PrivateChatId,
                        IsGroup = false,
                        Title = null,
                        Users = Users,
                    },
                    UsersToAdd = Users,
                    IncomingQuery = new()
                    {
                        UserId = StrangerUserId,
                        ChatId = PrivateChatId,
                    },
                    ExceptionType = typeof(AccessDeniedException),
                    ExceptionMessage = "Access denied.",
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
