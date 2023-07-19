using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace basic.Models
{
  public class Auth
  {
    public string PasswordHash { get; set; } = "";
    public string PasswordSalt { get; set; } = "";
  }
}