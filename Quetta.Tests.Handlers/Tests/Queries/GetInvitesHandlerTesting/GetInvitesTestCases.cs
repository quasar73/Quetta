using Quetta.Common.Enums;
using Quetta.Common.Models.Responses;
using Quetta.Data.Models;
using System.Collections;

namespace Quetta.Tests.Handlers.Queries
{
    public class GetInvitesTestCases : IEnumerable<object[]>
    {
        private const string ReceiverId = "00000000-0000-0000-0000-000000000000";
        private readonly User Sender = new User()
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "senderusername",
            FirstName = "First",
            LastName = "Last"
        };

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new GetInvitesTestCaseModel()
                {
                    InvitesToAdd = new Invite[]
                    {
                        new Invite
                        {
                            Id = "00000000-0000-0000-0000-000000000000",
                            ReceiverId = ReceiverId,
                            Status = InviteStatus.Pending,
                            DateTime = new DateTime(2022, 01, 01),
                            IsGroupChat = false,
                            Sender = Sender,
                        },
                        new Invite
                        {
                            Id = "00000000-0000-0000-0000-000000000001",
                            ReceiverId = ReceiverId,
                            Status = InviteStatus.Pending,
                            DateTime = new DateTime(2022, 01, 02),
                            IsGroupChat = false,
                            Sender = Sender,
                        },
                        new Invite
                        {
                            Id = "00000000-0000-0000-0000-000000000002",
                            ReceiverId = ReceiverId,
                            Status = InviteStatus.Declined,
                            DateTime = new DateTime(2022, 01, 02),
                            IsGroupChat = false,
                            Sender = Sender,
                        },
                        new Invite
                        {
                            Id = "00000000-0000-0000-0000-000000000003",
                            ReceiverId = "00000000-0000-0000-0000-000000000001",
                            Status = InviteStatus.Pending,
                            DateTime = new DateTime(2022, 01, 01),
                            IsGroupChat = false,
                            Sender = Sender,
                        },
                    },
                    UsersToAdd = new User[]
                    {
                        Sender
                    },
                    IncomingQuery = new(ReceiverId),
                    ExpectedReturnedInvites = new InviteResponse[]
                    {
                        new InviteResponse()
                        {
                            SenderUsername = "senderusername",
                            InviteId = "00000000-0000-0000-0000-000000000000",
                            DateTime = new DateTime(2022, 01, 01),
                            ChatId = null,
                            IsGroupChat = false,
                        },
                        new InviteResponse()
                        {
                            SenderUsername = "senderusername",
                            InviteId = "00000000-0000-0000-0000-000000000001",
                            DateTime = new DateTime(2022, 01, 02),
                            ChatId = null,
                            IsGroupChat = false,
                        },
                    }
                }
            };
            yield return new object[]
            {
                new GetInvitesTestCaseModel()
                {
                    InvitesToAdd = Array.Empty<Invite>(),
                    UsersToAdd =  Array.Empty<User>(),
                    IncomingQuery = new(ReceiverId),
                    ExpectedReturnedInvites = Array.Empty<InviteResponse>(),
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
