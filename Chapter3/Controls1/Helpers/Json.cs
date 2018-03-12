using System;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Controls1.Helpers
{
    public static class Json
    {
        public static async Task<T> ToObjectAsync<T>(string value)
        {
#pragma warning disable IDE0022 // Use expression body for methods
            return await Task.Run<T>(() =>
            {
                return JsonConvert.DeserializeObject<T>(value);
            });
#pragma warning restore IDE0022 // Use expression body for methods
        }

        public static async Task<string> StringifyAsync(object value)
        {
#pragma warning disable IDE0022 // Use expression body for methods
            return await Task.Run<string>(() =>
            {
                return JsonConvert.SerializeObject(value);
            });
#pragma warning restore IDE0022 // Use expression body for methods
        }
    }
}
