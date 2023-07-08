namespace basic.Models;
public partial class UserJobInfo
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string JobTitle { get; set; } = "";
    public string Department { get; set; } = "";

}