namespace Demo.JsonConverter;

public class Node
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string ParentId { get; set; }

    public List<Node> ChildNodes { get; set; } = new();

    public override string ToString()
    {
        return $"{{ID:{this.Id},Name:{this.Name},ParentId:{this.ParentId}}}";
    }
}
