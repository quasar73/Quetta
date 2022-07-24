using AutoMapper;
using Quetta.Data.Models;
using Quetta.Common.Enums;
using Quetta.Common.Models.Responses;

namespace Quetta.Data.Mapping
{
    public class ChatProfile : Profile
    {
        public ChatProfile()
        {
            CreateMap<Chat, ChatItemResponse>()
                .ForMember(dest => dest.ChatType, opt => opt.MapFrom(src => src.IsGroup ? ChatType.GroupChat : ChatType.PersonalChat))
                .ForMember(dest => dest.Title, opt => opt.Ignore());
        }
    }
}
