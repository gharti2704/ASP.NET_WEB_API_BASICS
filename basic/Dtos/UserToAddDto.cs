using Microsoft.EntityFrameworkCore;

namespace basic.Dtos;
[Keyless]
public class UserToAddDto
{
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Gender { get; set; } = "";
    public string Email { get; set; } = "";
    public bool Active { get; set; }
}