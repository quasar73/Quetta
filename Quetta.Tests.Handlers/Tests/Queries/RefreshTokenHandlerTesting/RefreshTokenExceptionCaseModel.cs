using Quetta.Common.Models.Queries;
using Quetta.Data.Models;

namespace Quetta.Tests.Handlers.Queries
{
    public class RefreshTokenExceptionCaseModel
    {
        public RefreshToken[] RefreshTokensToAdd { get; set; }

        public RefreshTokenQuery IncomingQuery { get; set; }

        public Type ExceptionType { get; set; }

        public string ExceptionMessage { get; set; }
    }
}
