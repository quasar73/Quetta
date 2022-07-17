﻿
using Common.Models.Responses;
using MediatR;

namespace Common.Models.Queries
{
    public class GetInvitesQuery : IRequest<ICollection<InviteResponse>>
    {
        public string UserId { get; set; }

        public GetInvitesQuery(string userId)
        {
            UserId = userId;
        }
    }
}
