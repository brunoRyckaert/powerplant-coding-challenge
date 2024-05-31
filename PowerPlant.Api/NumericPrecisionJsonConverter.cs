using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PowerPlant.Api
{
    public class NumericPrecisionJsonConverter : JsonConverter<decimal>
    {
        public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
        {
            writer.WriteRawValue(string.Format(CultureInfo.InvariantCulture, "{0:F1}", value));
        }
    }
}
