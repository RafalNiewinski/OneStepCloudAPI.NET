﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI
{
    class TolerantEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            Type type = IsNullableType(objectType) ? Nullable.GetUnderlyingType(objectType) : objectType;
            return objectType.GetTypeInfo().IsEnum;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            bool isNullable = IsNullableType(objectType);
            Type enumType = isNullable ? Nullable.GetUnderlyingType(objectType) : objectType;

            string[] names = Enum.GetNames(enumType);

            if(reader.TokenType == JsonToken.String)
            {
                string enumText = reader.Value.ToString();

                if(!string.IsNullOrEmpty(enumText))
                {
                    // If value contains forbidden first character (not letter) prepend with 'X'
                    if (!char.IsLetter(enumText[0]))
                        enumText = $"X{enumText}";

                    string match = names
                        .Where(n => string.Equals(n, enumText, StringComparison.OrdinalIgnoreCase))
                        .FirstOrDefault();

                    if (match != null)
                        return Enum.Parse(enumType, match);
                }
            }
            else if(reader.TokenType == JsonToken.Integer)
            {
                int enumVal = Convert.ToInt32(reader.Value);
                int[] values = (int[])Enum.GetValues(enumType);
                if (values.Contains(enumVal))
                    return Enum.Parse(enumType, enumVal.ToString());
            }

            if(!isNullable)
            {
                string defaultName = names
                    .Where(n => string.Equals(n, "Unknown", StringComparison.OrdinalIgnoreCase))
                    .FirstOrDefault();

                if (defaultName == null)
                    throw new JsonSerializationException("Cannot deserialize enum because of it has not defined value and defined enum type do not support Unknown value and is not nullable.");

                return Enum.Parse(enumType, defaultName);
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            string data = value.ToString();

            // If value contains preprended first character 'X' before forbidden char remove it
            if (data.Length > 1 && data[0] == 'X' && !char.IsLetter(data[1]))
                data = data.Substring(1);

            writer.WriteValue(data);
        }

        private bool IsNullableType(Type t)
        {
            return (t.GetTypeInfo().IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }
    }
}
