namespace Common.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public class PermissionAttribute : Attribute
{

    public string Name { get; }

    public PermissionAttribute(string name)
    {        
        Name = name;
    }
}
