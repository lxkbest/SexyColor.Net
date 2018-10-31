using System.Collections.Generic;

namespace SexyColor.Infrastructure
{
    public static class DictionaryExtension
    {
        public static T Get<T>(this IDictionary<string, object> dictionary, string key, T defaultValue)
        {
            if (dictionary.ContainsKey(key))
            {
                object obj;
                dictionary.TryGetValue(key, out obj);
                return ValueUtility.ChangeType<T>(obj, defaultValue);
            }
            return defaultValue;
        }

        public static T Get<T>(this IDictionary<int, string> dictionary, int key, T defaultValue)
        {
            if (dictionary.ContainsKey(key))
            {
                string obj;
                dictionary.TryGetValue(key, out obj);
                return ValueUtility.ChangeType<T>(obj, defaultValue);
            }
            return defaultValue;
        }
    }
}
