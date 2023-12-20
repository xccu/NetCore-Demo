using Newtonsoft.Json.Linq;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Demo.JsonConverter;

internal class NodesJsonConverter : JsonConverter<Nodes>
{
    public override Nodes? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var nodes = new Nodes();

        if (reader.TokenType != JsonTokenType.StartArray)
        {
            return nodes;
        }
        //reader.Read()

        JsonObject obj= new JsonObject();
        //StringBuilder stringBuilder = new StringBuilder();
        var node = new Node();
        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject || reader.TokenType == JsonTokenType.EndArray)
                break;
            ReadNode(ref reader, node);
            nodes.Add(node);
            node = new Node();
        }

        return nodes;
    }

    private void ReadNode(ref Utf8JsonReader reader, Node node)
    {
        while (true)
        {
            SkipStart(ref reader);
            if (CheckEnd(reader)) 
            {
                return;
            }
            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                
                ReadId(ref reader, node);               //end with JsonTokenType.PropertyName
                ReadName(ref reader, node);       //end with JsonTokenType.PropertyName
                ReadParentId(ref reader, node);   //end with JsonTokenType.PropertyName
                //ReadChildNodes(ref reader, node); //end with JsonTokenType.EndObject
            }
            if (CheckEnd(reader))
            {
                //reader.Read();
                return;
            }
        }
        
    }

    private void ReadId(ref Utf8JsonReader reader, Node node)
    {
        if (reader.ValueTextEquals(JsonEncodedText.Encode("Id").EncodedUtf8Bytes))
        {
            reader.Read();
            node.Id = reader.GetString();
            Console.WriteLine(node.Id);
            reader.Read(); 
        }           
    }

    private void ReadName(ref Utf8JsonReader reader, Node value)
    {
        if (reader.ValueTextEquals(JsonEncodedText.Encode("Name").EncodedUtf8Bytes))
        {
            reader.Read();
            value.Name = reader.GetString();
            Console.WriteLine(value.Name);
            reader.Read();
        }
    }

    private void ReadParentId(ref Utf8JsonReader reader, Node value)
    {
        if (reader.ValueTextEquals(JsonEncodedText.Encode("ParentId").EncodedUtf8Bytes))
        {
            reader.Read();
            value.ParentId = reader.GetString();
            Console.WriteLine(value.ParentId);
            reader.Read();
        }
    }

    private void ReadChildNodes(ref Utf8JsonReader reader, Node value)
    {
        if (reader.ValueTextEquals(JsonEncodedText.Encode("ChildNodes").EncodedUtf8Bytes))
        {           
            reader.Read();
            var chlidNode = new Node();
            ReadNode(ref reader, chlidNode);
            //value.ChildNodes.Add(chlidNode);
            Console.WriteLine(chlidNode.ToString());
            reader.Read();
        }
    }

    private bool TryReadStringProperty(ref Utf8JsonReader reader, JsonEncodedText propertyName, [NotNullWhen(true)] out string? value)
    {
        SkipStart(ref reader);
        if (reader.ValueTextEquals(propertyName.EncodedUtf8Bytes))
        {
            reader.Read();
            value = reader.GetString();
            return true;
        }
        value = null;
        return false;
    }

    private void SkipStart(ref Utf8JsonReader reader)
    {
        if (reader.TokenType == JsonTokenType.StartArray)
        {
            reader.Read();
        }
        if (reader.TokenType == JsonTokenType.StartObject)
        {
            reader.Read();
        }
    }

    private bool CheckEnd(Utf8JsonReader reader)
    {
        return (reader.TokenType == JsonTokenType.EndObject || reader.TokenType == JsonTokenType.EndArray);     
    }

    public override void Write(Utf8JsonWriter writer, Nodes value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }


}
