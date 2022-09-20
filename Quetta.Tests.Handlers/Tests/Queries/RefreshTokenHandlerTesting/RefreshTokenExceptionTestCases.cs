using Quetta.Common.Exceptions;
using Quetta.Data.Models;
using System.Collections;

namespace Quetta.Tests.Handlers.Queries
{
    public class RefreshTokenExceptionTestCases : IEnumerable<object[]>
    {
        private const string Token = "AAAAABBBBBCCCCC";
        private const string ExistingUserId = "00000000-0000-0000-0000-000000000000";
        private const string NonexistentUserId = "00000000-0000-0000-0000-000000000001";

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new RefreshTokenExceptionCaseModel()
                {
                    RefreshTokensToAdd = Array.Empty<RefreshToken>(),
                    IncomingQuery = new(Token),
                    ExceptionType = typeof(InvalidTokenException),
                    ExceptionMessage = "The token is invalid.",
                },
            };
            yield return new object[]
            {
                new RefreshTokenExceptionCaseModel()
                {
                    RefreshTokensToAdd = new RefreshToken[]
                    {
                        new RefreshToken()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Token = Token,
                            Expires = DateTime.UtcNow - TimeSpan.FromHours(1),
                            UserId = ExistingUserId,
                        }
                    },
                    IncomingQuery = new("AAAAABBBBBCCCCC"),
                    ExceptionType = typeof(TokenExpiredException),
                    ExceptionMessage = "Token is expired.",
                },
            };
            yield return new object[]
            {
                new RefreshTokenExceptionCaseModel()
                {
                    RefreshTokensToAdd = new RefreshToken[]
                    {
                        new RefreshToken()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Token = Token,
                            Expires = DateTime.UtcNow + TimeSpan.FromHours(1),
                            UserId = NonexistentUserId,
                        }
                    },
                    IncomingQuery = new("AAAAABBBBBCCCCC"),
                    ExceptionType = typeof(InvalidTokenException),
                    ExceptionMessage = "The token is invalid.",
                },
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
