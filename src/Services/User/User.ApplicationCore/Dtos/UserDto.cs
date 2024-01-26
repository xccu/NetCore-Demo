using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.ApplicationCore.Dtos;

public class UserDto
{
    public string id { get; set; }
    public String name { get; set; }
    public String password { get; set; }
    public int age { get; set; }
    public String gender { get; set; }
    public String race { get; set; }
    public int VersionNo { get; set; }
}
