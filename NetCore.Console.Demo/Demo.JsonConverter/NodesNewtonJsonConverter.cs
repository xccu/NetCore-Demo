using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Demo.JsonConverter;

public class NodesNewtonJsonConverter : JsonConverter<Nodes>
{
    public override Nodes? ReadJson(JsonReader reader, Type objectType, Nodes? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var nodes = new Nodes();
        if (reader.TokenType != JsonToken.StartArray) return nodes;

        reader.Read();
        if (reader.TokenType == JsonToken.EndArray) return nodes;

        do
        {
            var jobNode = CreateFromJson(JObject.Load(reader));         
            nodes.Add(jobNode);

        } while (reader.Read() && reader.TokenType == JsonToken.StartObject);

        return nodes;
    }

    public override void WriteJson(JsonWriter writer, Nodes? value, JsonSerializer serializer)
    {
        //throw new NotImplementedException();
    }

    private Node CreateFromJson(JObject jObj)
    {
        var id = jObj[nameof(Node.Id)] ?? "";
        var name = jObj[nameof(Node.Name)] ?? "";
        var parentId = jObj[nameof(Node.ParentId)] ?? "";
        return new() {Id = id.ToString(), Name = name.ToString(), ParentId = parentId.ToString() };
    }
}