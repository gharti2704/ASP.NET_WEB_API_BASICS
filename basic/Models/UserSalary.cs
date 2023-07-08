namespace basic.Models;
public partial class UserSalary
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public decimal Salary { get; set; }
    public decimal AverageSalary { get; set; }

}