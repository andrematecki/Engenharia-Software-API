using Engenharia_Software.Domain.CrossCutting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engenharia_Software.CrossCutting
{
    public class JsonConverter : IJsonConverter
    {
        private JsonSerializerSettings _jsonSerializerSettings;

        public JsonConverter()
        {
            _jsonSerializerSettings= new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Local,
                DateParseHandling = DateParseHandling.DateTimeOffset
            };
        }

        public T DeserializeObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, _jsonSerializerSettings);
        }

        public string SerializeObject(object value)
        {
            return JsonConvert.SerializeObject(value, _jsonSerializerSettings);
        }
    }
}
