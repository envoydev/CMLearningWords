using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace CMLearningWords.WebUI.Extensions
{
    public static class TempDataExtensions
    {
        public static void PutList<T>(this ITempDataDictionary tempData, string key, T objects) where T : class
        {
            tempData[key] = JsonConvert.SerializeObject(objects);
        }

        public static T GetList<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            object obj;
            tempData.TryGetValue(key, out obj);
            return obj == null ? null : JsonConvert.DeserializeObject<T>((string)obj);
        }
    }
}
