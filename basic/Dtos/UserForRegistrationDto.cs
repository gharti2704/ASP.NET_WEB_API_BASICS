namespace basic.Dtos;
public partial class UserForRegistrationDto
{
  public string Email { get; set; } = null!;
  public string Password { get; set; } = null!;
  public string ConfirmPassword { get; set; } = null!;
}