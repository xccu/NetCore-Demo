namespace Demo.AutoMapper.Models;

public class UserModel
{
    public int Id { set; get; }

    public string UserName { set; get; }

    public string PassWord { set; get; }

    public List<string> AssignRoles { set; get; } = new List<string>();
}
