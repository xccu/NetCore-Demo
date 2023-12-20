//using Newtonsoft.Json;
using Newtonsoft.Json;
using System.Text.Json;

namespace Demo.JsonConverter;

public class JsonConverterDemo
{
    public static void Run()
    {
        //https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/converters-how-to?pivots=dotnet-7-0
        //https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/supported-collection-types
        try
        {           
            var deserializeOptions = new JsonSerializerOptions();
            deserializeOptions.Converters.Add(new NodesJsonConverter());
            var jsonString = getJsonString();

            //var m = JsonConvert.DeserializeObject<Nodes>(jsonString);
            //var nodes = System.Text.Json.JsonSerializer.Deserialize<Nodes>(jsonString, deserializeOptions);

            var nodes = System.Text.Json.JsonSerializer.Deserialize<Nodes>(jsonString);
            foreach (var item in nodes)
            {
                Console.WriteLine(item.ToString());
            }
            //var menu = JsonConvert.DeserializeObject<Nodes>(jsonString);
        }
        catch (Exception ex)
        {
            
        }
        
    }

    private static string getJsonString()
    {
        Node node1 = new() { Id = "1", Name = "Node-1", ParentId = null };
        Node node2 = new() { Id = "2", Name = "Node-2", ParentId = "1" };
        Node node3 = new() { Id = "3", Name = "Node-3", ParentId = "1" };
        Node node4 = new() { Id = "4", Name = "Node-4", ParentId = "2" };

        node1.ChildNodes.Add(node2);
        node1.ChildNodes.Add(node3);
        node2.ChildNodes.Add(node4);

        List<Node> list = new List<Node>()
        {
           node1,node2,node3,node4
        };

        var menu =new Nodes(list);
        return System.Text.Json.JsonSerializer.Serialize(menu);
    }
}
