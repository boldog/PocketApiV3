using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace PocketApiV3
{
    class ApiSerializer
    {
        public static readonly ApiSerializer Instance = new ApiSerializer();

        readonly JsonSerializerSettings _jsonSerializerSettings;

        ApiSerializer()
        {
            _jsonSerializerSettings = new JsonSerializerSettings
            {
                Converters =
                {
                    new BooleanConverter(),
                    //new DateTimeUnixSecondsConverter(),
                    new DateTimeOffsetUnixSecondsConverter(),
                    new NullableBooleanConverter(),
                    new NullableDateTimeOffsetUnixSecondsConverter(),
                    new IPocketActionResultJsonConverter(),
                    new PocketItemStatusConverter(),
                    new StringEnumConverter { CamelCaseText = false }
                },
                Error = (object sender, ErrorEventArgs e) =>
                {
                    throw new PocketClientException(string.Format("Parse error: {0}", e.ErrorContext.Error.Message));
                },

                NullValueHandling = NullValueHandling.Ignore,
            };
        }

        public object Deserialize(string json, Type type) =>
            JsonConvert.DeserializeObject(json, type, _jsonSerializerSettings);

        public T Deserialize<T>(string json) =>
            JsonConvert.DeserializeObject<T>(json, _jsonSerializerSettings);

        public string SerializeToString(object obj, bool indented = false) =>
            JsonConvert.SerializeObject(obj, indented ? Formatting.Indented : Formatting.None, _jsonSerializerSettings);

        public JObject SerializeToJObject(object obj) =>
            JObject.FromObject(obj, JsonSerializer.Create(_jsonSerializerSettings));


        /// <summary>
        /// Non-nullable booleans are JSON integers.
        /// </summary>
        class BooleanConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType) =>
                objectType == typeof(bool);

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                var result = Convert.ToBoolean(reader.Value);
                return result;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                var x = (bool)value;
                writer.WriteValue(x ? 1 : 0);
            }
        }

        //class DateTimeUnixSecondsConverter : JsonConverter
        //{
        //    public override bool CanConvert(Type objectType) =>
        //        objectType == typeof(DateTime);

        //    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        //    {
        //        if (reader.Value == null)
        //            return default(DateTime);

        //        long seconds = Convert.ToInt64(reader.Value);
        //        return DateTimeOffset.FromUnixTimeSeconds(seconds).DateTime;
        //    }

        //    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        //    {
        //        var typedValue = (DateTime)value;
        //        var dateTimeOffset = new DateTimeOffset(typedValue);
        //        writer.WriteValue(dateTimeOffset.ToUnixTimeSeconds());
        //    }
        //}

        class DateTimeOffsetUnixSecondsConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType) =>
                objectType == typeof(DateTimeOffset);

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                if (reader.Value == null)
                    return default(DateTimeOffset);

                long seconds = Convert.ToInt64(reader.Value);
                return DateTimeOffset.FromUnixTimeSeconds(seconds);
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                var typedValue = (DateTimeOffset)value;
                writer.WriteValue(typedValue.ToUnixTimeSeconds());
            }
        }

        class IPocketActionResultJsonConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType) =>
                objectType == typeof(IPocketActionResult);

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                switch (reader.TokenType)
                {
                    case JsonToken.StartObject:
                        {
                            var result = serializer.Deserialize<AddActionResult>(reader);
                            return result;
                        }

                    case JsonToken.Boolean:
                        {
                            var boolValue = serializer.Deserialize<bool>(reader);
                            var result = new BooleanActionResult { Success = boolValue };
                            return result;
                        }

                    default:
                        throw new PocketJsonException($"Unexpected JSON token type {reader.TokenType}");
                        //return serializer.Deserialize(reader, objectType);
                }
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                serializer.Serialize(writer, value);
            }
        }

        class NullableBooleanConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType) =>
                objectType == typeof(bool?);

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {

                if (reader.Value == null)
                    return default(bool?);

                switch (reader.Value.ToString())
                {
                    case "0":
                        return false;
                    case "1":
                        return true;
                    default:
                        //throw new JsonSerializationException("The value to be written must be of type bool?.");
                        return null;
                }
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                if (value is bool?)
                {
                    var x = (bool?)value;
                    if (x.HasValue)
                        writer.WriteValue(x.Value ? "1" : "0");
                }
                else
                    throw new JsonSerializationException("The value to be written must be of type bool?.");
            }
        }

        class NullableDateTimeOffsetUnixSecondsConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType) =>
                objectType == typeof(DateTimeOffset?);

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                //if (reader.Value == null)
                //    return null;

                if (reader.Value == null)
                    return default(DateTimeOffset?);

                long seconds = Convert.ToInt64(reader.Value);
                return DateTimeOffset.FromUnixTimeSeconds(seconds);
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                var x = (DateTimeOffset?)value;
                if (x.HasValue)
                    writer.WriteValue(x.Value.ToUnixTimeSeconds().ToString());
            }
        }

        class PocketItemStatusConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType) =>
                objectType == typeof(PocketItemStatus);

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                string valueStr = reader.Value?.ToString() ?? "";
                switch (valueStr)
                {
                    case "0":
                        return PocketItemStatus.Normal;
                    case "1":
                        return PocketItemStatus.Archived;
                    case "2":
                        return PocketItemStatus.Deleted;
                    default:
                        throw new JsonSerializationException($"Unexpected item status value \"{valueStr}\".");
                }
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                var valueTyped = (PocketItemStatus)value;
                var valueCastToInt = (int)valueTyped;
                writer.WriteValue(valueCastToInt.ToString());
            }
        }
    }
}


