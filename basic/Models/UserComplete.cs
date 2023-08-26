using System.ComponentModel.DataAnnotations.Schema;

namespace basic.Models;
public partial class UserComplete
{
  public int UserId { get; set; }
  public string FirstName { get; set; } = "";
  public string LastName { get; set; } = "";
  public string Gender { get; set; } = "";
  public string Email { get; set; } = "";
  public bool Active { get; set; } = true;
  public string JobTitle { get; set; } = "";
  public string Department { get; set; } = "";
  [Column(TypeName = "decimal(5, 2)")] public decimal Salary { get; set; } = 0;
  [Column(TypeName = "decimal(5, 2)")] public decimal AverageSalary { get; set; } = 0;
}