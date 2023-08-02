namespace basic.Dtos;
public partial class UserForRegistrationDto
{
  public string FirstName { get; set; } = "";
  public string LastName { get; set; } = "";
  public string Gender { get; set; } = "";
  public string Email { get; set; } = null!;
  public string Password { get; set; } = null!;
  public string ConfirmPassword { get; set; } = null!;
}