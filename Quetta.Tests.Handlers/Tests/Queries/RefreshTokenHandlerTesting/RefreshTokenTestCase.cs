using Quetta.Data.Models;
using System.Collections;

namespace Quetta.Tests.Handlers.Queries
{
    public class RefreshTokenTestCase : IEnumerable<object[]>
    {
        private const string Token = "AAAAABBBBBCCCCC";
        private const string ExistingUserId = "00000000-0000-0000-0000-000000000000";

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new RefreshTokenTestCaseModel()
                {
                    RefreshTokensToAdd = new RefreshToken[]
                    {
                        new RefreshToken()
                        {
                            Id = Guid.NewGuid().ToString(),
                            UserId = ExistingUserId,
                            Token = Token,
                            Expires = DateTime.UtcNow + TimeSpan.FromHours(24),
                        },
                    },
                    ExpectedTokens = new()
                    {
                        AccessToken = "SOMEACCESSTOKEN",
                        RefreshToken = "SOMEREFRESHTOKEN",
                    },
                    IncomingQuery = new(Token),
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
