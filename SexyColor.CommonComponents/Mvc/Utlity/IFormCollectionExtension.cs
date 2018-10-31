using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.ComponentModel;
using System.Reflection;

namespace SexyColor.CommonComponents.Mvc.Utlity
{
    public static class IFormCollectionExtension
    {
        public static T Get<T>(this IFormCollection collection, string key)
        {
            if (typeof(T) == typeof(string))
                return Get<T>(collection, key, (T)Convert.ChangeType(string.Empty, typeof(T)));
            else
                return Get<T>(collection, key, default(T));
        }

        public static T Get<T>(this IFormCollection collection, string key, T defaultValue)
        {
            T returnValue = defaultValue;
 
            Type tType = typeof(T);
            TypeInfo tTypeInfo = tType.GetTypeInfo();
            if (tTypeInfo.IsGenericType && tType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                if (string.IsNullOrEmpty(collection[key].ToString()))
                    return defaultValue;
                return (T)TypeDescriptor.GetConverter(Nullable.GetUnderlyingType(tType)).ConvertFrom(collection[key]);
            }
            else if (tTypeInfo.IsEnum)
            {

                return (T)Enum.Parse(tType, collection[key]);
            }
            else
            {
                try
                {
                    return (T)Convert.ChangeType(collection[key], tType);
                }
                catch
                {
                    return returnValue;
                }
            }
        }
    }
}
