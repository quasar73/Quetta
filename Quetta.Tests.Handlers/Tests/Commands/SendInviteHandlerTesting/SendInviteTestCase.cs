using System.Collections;

namespace Quetta.Tests.Handlers.Commands
{
    public class SendInviteTestCase : IEnumerable<object[]>
    {
        private const string SenderId = "00000000-0000-0000-0000-000000000000";
        private const string ReceiverId = "00000000-0000-0000-0000-000000000001";

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new SendInviteTestCaseModel()
                {
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
                    SenderId = SenderId,
                    ReceiverId = ReceiverId,
                },
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
