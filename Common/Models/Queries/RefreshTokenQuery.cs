using MediatR;

namespace Common.Models.Queries
{
    public class RefreshTokenQuery : IRequest<TokenModel>
    {
        public string RefreshToken { get; set; }

        public RefreshTokenQuery(string refreshToken)
        {
            RefreshToken = refreshToken;
        }
    }
}
