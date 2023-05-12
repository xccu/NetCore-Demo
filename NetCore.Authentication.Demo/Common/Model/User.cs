using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model;

public class User
{
    public string Name { get; set; } = "user1";
    public DateTime BirthDate { get; set; } = DateTime.Now.AddYears(-20);
}
