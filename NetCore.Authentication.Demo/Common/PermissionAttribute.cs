using Microsoft.AspNetCore.Authorization;

namespace Common;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public class PermissionAttribute : Attribute, IAuthorizeData
{

    public PermissionAttribute(string Policy)
    {
        this.Policy = Policy;
    }

    public string? Policy { get; set; }
    public string? Roles { get; set; }
    public string? AuthenticationSchemes { get; set; }
}
