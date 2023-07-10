using AutoMapper;
using basic.Models;
using basic.Dtos;

namespace basic.Profiles;
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserToAddDto>().ReverseMap();
    }

}