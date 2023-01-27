using AutoMapper;
using Quetta.Common.Models.Responses;
using Quetta.Data.Models;

namespace Quetta.Data.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, ReaderResponse>()
                .ForMember(
                    dest => dest.FullName,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}")
                )
                .ForMember(dest => dest.AvatarUrl, opt => opt.MapFrom(src => src.AvatarUrl));
        }
    }
}
