using System.Collections;

namespace Quetta.Tests.Handlers.Commands
{
    public class SendMessageTestCase : IEnumerable<object[]>
    {
        private const string SendreId = "00000000-0000-0000-0000-000000000000";
        private const string ChatId = "00000000-0000-0000-0000-000000000000";
        private const string TestText = "This is test text";

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new SendMessageTestCaseModel()
                {
                    IncomingCommand = new()
                    {
                        SenderId = SendreId,
                        ChatId = ChatId,
                        Date = new DateTime(2022, 01, 01),
                        Text = TestText,
                    },
                    ExpectedResult = new()
                    {
                         ChatId = ChatId,
                         UserId = SendreId,
                         Date = new DateTime(2022, 01, 01),
                         Text = "Encrypted message.",
                         SecretVersion = "actualSecret",
                    }
                },
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
