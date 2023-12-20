using Newtonsoft.Json;
using System.Collections;

namespace Demo.JsonConverter;

//[JsonConverter(typeof(NodesJsonConverter))]
public class Nodes : ICollection<Node>//IEnumerable<Node>
{
    List<Node> _nodes { get; set; }

    public int Count => _nodes.Count;

    public bool IsReadOnly => false;

    public IEnumerator<Node> GetEnumerator() => this._nodes.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)this._nodes).GetEnumerator();

    public void Add(Node node) => _nodes.Add(node);

    public void Clear() => _nodes.Clear();


    public bool Contains(Node item) => _nodes.Contains(item);

    public void CopyTo(Node[] array, int arrayIndex) => _nodes.CopyTo(array, arrayIndex);

    public bool Remove(Node item) => _nodes.Remove(item);

    public Nodes(List<Node> items) => _nodes = items;

    public Nodes() => _nodes = new List<Node>();
}
