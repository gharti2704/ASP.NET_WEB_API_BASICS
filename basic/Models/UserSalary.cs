using System.ComponentModel.DataAnnotations.Schema;

namespace basic.Models;
public partial class UserSalary
{
  public int UserId { get; set; }
  [Column(TypeName = "decimal(5, 2)")]
  public decimal Salary { get; set; }
  [Column(TypeName = "decimal(5, 2)")]
  public decimal AverageSalary { get; set; }

}