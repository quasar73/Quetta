﻿using AutoMapper;
using Quetta.Common.Models.Responses;
using Quetta.Data.Models;

namespace Quetta.Data.Mapping
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, MessageResponse>()
               .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.UserName))
               .ForMember(dest => dest.Readers, opt => opt.MapFrom(src => src.WhoRead));

            CreateMap<Message, SidebarResponse>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.UserName));
        }
    }
}
