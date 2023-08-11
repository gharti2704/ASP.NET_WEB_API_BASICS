using AutoMapper;
using basic.Models;
using basic.Dtos;

namespace basic.Profiles;

public class PostProfile : Profile
{
    public PostProfile()
    {
        CreateMap<Post, PostToAddDto>().ReverseMap();
    }
}