using Quetta.Common.Enums;
using Quetta.Common.Exceptions;
using Quetta.Data.Models;
using System.Collections;

namespace Quetta.Tests.Handlers.Commands
{
    public class SendInviteExceptionTestCase : IEnumerable<object[]>
    {
        private const string SenderId = "00000000-0000-0000-0000-000000000000";
        private const string ReceiverId = "00000000-0000-0000-0000-000000000001";
        private const string NonexistentUsername = "username3";

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new SendInviteExceptionCaseModel()
                {
                    InvitesToAdd = Array.Empty<Invite>(),
                    IncomingCommand = new()
                    {
                        SendInviteRequest = new()
                        {
                            ChatId = null,
                            IsGroupChat = false,
                            ReceiverUsername = NonexistentUsername,
                        },
                        SenderId = SenderId,
                    },
                    ExceptionType = typeof(EntityNotFoundException),
                    ExceptionMessage = "User not found.",
                },
            };
            yield return new object[]
            {
                new SendInviteExceptionCaseModel()
                {
                    InvitesToAdd = new Invite[]
                    {
                        new Invite()
                        {
                            Id = Guid.NewGuid().ToString(),
                            IsGroupChat = false,
                            DateTime = DateTime.Now,
                            SenderId = SenderId,
                            ReceiverId = ReceiverId,
                            Status = InviteStatus.Pending,
                        }
                    },
                    IncomingCommand = new()
                    {
                        SendInviteRequest = new()
                        {
                            ChatId = null,
                            IsGroupChat = false,
                            ReceiverUsername = "username2",
                        },
                        SenderId = SenderId,
                    },
                    ExceptionType = typeof(UserAlreadyInvitedException),
                    ExceptionMessage = "User already invited.",
                },
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
