using AutoMapper;
using URLShortener.Dtos.UserDtos;
using URLShortener.Models;

namespace URLShortener.Helpers.MappingProfiles;

public class UserMappingProfiles : Profile
{
    public UserMappingProfiles()
    {
        // registeration
        CreateMap<CreateUserAppDto, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForAllMembers(opts => opts.Condition((_, _, srcMember) => srcMember != null));
    }
}
