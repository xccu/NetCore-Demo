using Common.ModelBinder;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Common.Model;

[ModelBinder(BinderType = typeof(UserModelBinder))]
public class UserModel
{
    public string Id { get; set; }
    public string Name { get; set; }

    public UserModel()
    {
        this.Id = System.Guid.NewGuid().ToString();
    }

    public UserModel(string id,string name)
    {
        this.Id = id;
        this.Name = name;
    }
}
