using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using basic.Models;
using basic.Data;
using basic.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;

namespace basic.Controllers;

[ApiController]
[Route("api")]
public class UserSalaryController : ControllerBase
{
    [HttpGet("salary")]
    public async Task<IActionResult> GetUserSalary()
    {
        try
        {
            await Task.Delay(1000);
            return Ok("100");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
};