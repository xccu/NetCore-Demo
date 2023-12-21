using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.AutoMapper.Dtos;

public class UserDto
{
    public int Id { set; get; }

    public string Name { set; get; }

    public string PassWord { set; get; }

    public List<string> Roles { set; get; } = new List<string>();
}
