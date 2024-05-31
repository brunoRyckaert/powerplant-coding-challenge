using System.Text.Json.Serialization;

namespace PowerPlant.Api.Helpers
{
    /// <summary>
    /// The default JsonConverterAttribute does not allow to pass a parameter to the JsonConverter to be constructed.
    /// This class represents an attempt to remedy this.
    /// </summary>
    /// <typeparam name="T">Type of the parameter that will be used in the converter</typeparam>
    public class JsonConverterAttribute<T> : JsonConverterAttribute
    {
        private Type type;
        private T parameter;

        public JsonConverterAttribute(Type type, T parameter)
        {
            this.type = type;
            this.parameter = parameter;
        }
        public override JsonConverter? CreateConverter(Type typeToConvert)
        {
            return Activator.CreateInstance(type, parameter) as JsonConverter;
        }
    }
}
