using Quetta.Common.Models.Queries;
using Quetta.Common.Models.Responses;
using Quetta.Data.Models;
using System.Collections;

namespace Quetta.Tests.Handlers.Queries
{
    public class ChatsIsNotFoundTestCase : IEnumerable<object[]>
    {
        private const string NonexistentUserId = "00000000-0000-0000-0000-000000000001";

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new GetChatsTestCaseModel()
                {
                    UsersToAdd = Array.Empty<User>(),
                    ChatsToAdd = new Chat[]
                    {
                        new Chat()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Users = new List<User>(),
                            Messages = new List<Message>(),
                        },
                        new Chat()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Users = new List<User>(),
                            Messages = new List<Message>(),
                        }
                    },
                    MessagesToAdd = Array.Empty<Message>(),
                    IncomingQuery = new GetChatsQuery(NonexistentUserId),
                    ExpectedReturnedChats = Array.Empty<ChatItemResponse>(),
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
