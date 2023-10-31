namespace Models;

public class Bar
{
    public int Id { get { return this.GetHashCode(); } }

    public string Name { get; set; } = "Bar";
}
