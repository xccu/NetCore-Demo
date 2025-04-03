namespace Ant_Menu_Demo.Pages;

public class Node
{
    public ICollection<Node> ChildNodes { get; set; } = [];
    public string Title { get; set; }
    public string Image { get; set; }
}