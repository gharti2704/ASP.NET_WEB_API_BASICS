using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace basic.Controllers;

[ApiController]
[Route("api")]
public class UserController : ControllerBase
{
    public UserController() { }

    [HttpGet("users")]
    public string GetUsers()
    {
        return "Hello World!";
    }
}