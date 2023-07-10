using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using basic.Models;
using basic.Data;
using basic.Dtos;

namespace basic.Controllers;

[ApiController]
[Route("api")]
public class UserJobInfoController
{
    private readonly IMapper _mapper;
    private readonly IUserJobInfoReposiotry _userJobInfoReposiotry;
    public UserJobInfoController(IMapper mapper, IUserJobInfoReposiotry userJobInfoReposiotry)
    {
        _mapper = mapper;
        _userJobInfoReposiotry = userJobInfoReposiotry;
    }

}