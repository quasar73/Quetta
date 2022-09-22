using Quetta.Common.Models;
using Quetta.Common.Models.Queries;
using Quetta.Data.Models;

namespace Quetta.Tests.Handlers.Queries
{
    public class RefreshTokenTestCaseModel
    {
        public RefreshToken[] RefreshTokensToAdd { get; set; }

        public RefreshTokenQuery IncomingQuery { get; set; }

        public TokenModel ExpectedTokens { get; set; }
    }
}
