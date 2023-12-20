using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.AutoMapper;

public class UserModel
{
    public string Id { set; get; }

    public string UserName { set; get; }

    public string PassWord { set; get; }

    public List<string> AssignRoles { set; get; } = new List<string>();
}
