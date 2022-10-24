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
                .ForMember(
                    dest => dest.ChatType,
                    opt =>
                        opt.MapFrom(src => src.IsGroup ? ChatType.GroupChat : ChatType.PersonalChat)
                )
                .ForMember(
                    dest => dest.LastMessage,
                    opt =>
                        opt.MapFrom(src => src.Messages.OrderBy(m => m.Date).LastOrDefault()!.Text)
                )
                .ForMember(
                    dest => dest.LastMessageSecretVersion,
                    opt =>
                        opt.MapFrom(
                            src => src.Messages.OrderBy(m => m.Date).LastOrDefault()!.SecretVersion
                        )
                )
                .ForMember(dest => dest.Title, opt => opt.Ignore());
        }
    }
}
