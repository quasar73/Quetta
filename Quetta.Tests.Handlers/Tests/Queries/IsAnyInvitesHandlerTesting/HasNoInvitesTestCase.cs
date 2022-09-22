using Quetta.Common.Enums;
using Quetta.Data.Models;
using System.Collections;

namespace Quetta.Tests.Handlers.Queries
{
    public class HasNoInvitesTestCase : IEnumerable<object[]>
    {
        private const string ReceiverId = "00000000-0000-0000-0000-000000000000";
        private const string SenderId = "00000000-0000-0000-0000-000000000001";

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new IsAnyInvitesTestCaseModel()
                {
                    InvitesToAdd = new Invite[]
                    {
                        new Invite()
                        {
                            Id = Guid.NewGuid().ToString(),
                            ReceiverId = ReceiverId,
                            Status = InviteStatus.Pending,
                            SenderId = SenderId,
                        },
                    },
                    IncomingQuery = new("00000000-0000-0000-0000-000000000001"),
                    ExpectedResult = new()
                    {
                        HasInvites = false,
                    },
                }
            };
            yield return new object[]
            {
                new IsAnyInvitesTestCaseModel()
                {
                    InvitesToAdd = new Invite[]
                    {
                        new Invite()
                        {
                            Id = Guid.NewGuid().ToString(),
                            ReceiverId = ReceiverId,
                            Status = InviteStatus.Accepted,
                            SenderId = SenderId,
                        },
                    },
                    IncomingQuery = new(ReceiverId),
                    ExpectedResult = new()
                    {
                        HasInvites = false,
                    },
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
