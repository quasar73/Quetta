using AutoMapper;
using Quetta.Common.Models.Responses;
using Quetta.Data.Models;

namespace Quetta.Data.Mapping
{
    public class InviteProfile : Profile
    {
        public InviteProfile()
        {
            CreateMap<Invite, InviteResponse>()
                .ForMember(dest => dest.IsGroupChat, opt => opt.MapFrom(src => src.IsGroupChat))
                .ForMember(dest => dest.SenderUsername, opt => opt.MapFrom(src => src.Sender.UserName))
                .ForMember(dest => dest.ChatId, opt => opt.MapFrom(src => src.ChatId))
                .ForMember(dest => dest.InviteId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
