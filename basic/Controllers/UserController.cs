using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Threading.Tasks;
using basic.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace basic.Controllers;

[ApiController]
[Route("api")]
public class UserController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public UserController(IConfiguration configuration)
    {
        _context = new ApplicationDbContext(configuration);
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _context.Users.ToListAsync();
        return Ok(users);
    }
}