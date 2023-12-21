namespace Demo.AutoMapper.Models;

public class NodeModel
{
    public string Id { get; set; }
    public string NodeName { get; set; }
    public List<NodeModel> SubNodes { get; set; } = new();
}
