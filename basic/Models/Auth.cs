namespace basic.Models
{
  public class Auth
  {
    public string? Email { get; set; }
    public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
    public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();
  }
}