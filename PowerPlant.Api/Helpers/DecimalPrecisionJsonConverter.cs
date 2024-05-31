using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PowerPlant.Api.Helpers
{
    /// <summary>
    /// Use this converter to serialize a decimal with the provided integer parameter as the precision
    /// </summary>
    public class DecimalPrecisionJsonConverter : JsonConverter<decimal>
    {
        private int precision = 0;
        public DecimalPrecisionJsonConverter(int precision)
        {
            this.precision = precision;
        }
        public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
        {
            writer.WriteRawValue(string.Format(CultureInfo.InvariantCulture, "{0:F" + precision + "}", value));
        }
    }
}
