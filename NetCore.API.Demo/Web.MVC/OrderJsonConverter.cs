using Common.Model;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Web.MVC;

public class OrderJsonConverter : JsonConverter<Order>
{
    public override Order Read(ref Utf8JsonReader reader,Type typeToConvert,JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            string str = reader.GetString();
            var result = (Order)Enum.Parse(typeof(Order), str);

            return result;
        }
            
        if (reader.TokenType == JsonTokenType.Number)
        {
            int value = reader.GetInt32();
            return (Order)value;
        }
           
        return Order.Unknown;
    }

    public override void Write(Utf8JsonWriter writer, Order value, JsonSerializerOptions options)
    {     
        writer.WriteNumberValue((int)(object)value);
        //var str = value.ToString();
        //writer.WriteStringValue(str);
    }
}
