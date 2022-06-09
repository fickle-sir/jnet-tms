using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JNet
{
    [TypeConverter(typeof(DimensionTypeConverter))]
    [JsonConverter(typeof(DimensionJsonConverter))]
    public sealed class Dimension<T> where T : struct, IComparable
    {
        public T Length { get; set; }

        public T Width { get; set; }

        public T Height { get; set; }

        public override string ToString() => $"{Length}*{Width}*{Height}";
    }

    internal class DimensionTypeConverter : TypeConverter
    {
        private readonly Type _type;
        private readonly Type _genericType;

        public DimensionTypeConverter(Type type)
        {
            _type = type;
            _genericType = GetGenericType(type);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value == null)
                return null;

            if (value is string str)
            {
                var converter = TypeDescriptor.GetConverter(_genericType);
                var parts = str.Split("*").Select(p => converter.ConvertFrom(p)).ToArray();

                var dimension = Activator.CreateInstance(_type);
                if (parts.Length > 0)
                    _type.GetProperty("Length").SetValue(dimension, parts[0]);
                if (parts.Length > 1)
                    _type.GetProperty("Width").SetValue(dimension, parts[1]);
                if (parts.Length > 2)
                    _type.GetProperty("Height").SetValue(dimension, parts[2]);

                return dimension;
            }

            return base.ConvertFrom(context, culture, value);
        }

        public static Type GetGenericType(Type type)
        {
            do
            {
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Dimension<>))
                    return type.GetGenericArguments()[0];
                else
                    type = type.BaseType;
            } while (type != typeof(object));
            return null;
        }
    }

    internal class DimensionJsonConverter : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert) => true;

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            var gType = DimensionTypeConverter.GetGenericType(typeToConvert);
            var type = typeof(DimensionJsonConverterOfT<>).MakeGenericType(gType);
            return Activator.CreateInstance(type) as JsonConverter;
        }

        private class DimensionJsonConverterOfT<T> : JsonConverter<Dimension<T>> where T : struct, IComparable
        {
            public override bool CanConvert(Type typeToConvert)
            {
                throw new NotImplementedException();
            }

            public override Dimension<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (reader.TokenType == JsonTokenType.Null)
                    return null;

                try
                {
                    if (reader.TokenType == JsonTokenType.String)
                        return (Dimension<T>)TypeDescriptor.GetConverter(typeToConvert).ConvertFrom(reader.GetString());
                    else
                        return JsonSerializer.Deserialize<Dimension<T>>(reader.GetString());
                }
                catch (Exception ex)
                {
                    throw new JsonException(ex.Message, ex);
                }
            }

            public override void Write(Utf8JsonWriter writer, Dimension<T> value, JsonSerializerOptions options)
            {
                if (value == null)
                {
                    writer.WriteNullValue();
                }
                else
                {
                    writer.WriteStringValue(value.ToString());
                }
            }
        }
    }
}
