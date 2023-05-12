using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Boek.Core.Extensions
{
    public static class JsonConverterExtension
    {
        public static string ToJson<T>(this T o)
        {
            return JsonConvert.SerializeObject(o, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }

        public static T ToObject<T>(this string json)
        {
            return json != null ? JsonConvert.DeserializeObject<T>(json) : default;
        }
    }
}
