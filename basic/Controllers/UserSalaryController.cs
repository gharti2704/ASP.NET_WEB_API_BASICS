using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using basic.Models;
using basic.Data;
using basic.Data.Repositories.Salary;
using basic.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;

namespace basic.Controllers;

[ApiController]
[Route("api")]
public class UserSalaryController : ControllerBase
{
    private readonly IUserSalaryRepository _salaryRepository;
    public UserSalaryController(IUserSalaryRepository salaryRepository)
    {
        _salaryRepository = salaryRepository;
    }
    
    [HttpGet("salary")]
    public async Task<IActionResult> GetUserSalary()
    {
        try
        {
            var salaryItems = await _salaryRepository.GetUserSalaries();
            return StatusCode(200, salaryItems);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
};