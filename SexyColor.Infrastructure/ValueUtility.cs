using System;
using System.Reflection;

namespace SexyColor.Infrastructure
{
    public static class ValueUtility
    {
        public static T ChangeType<T>(object value)
        {
            return ChangeType<T>(value, default(T));
        }

        public static T ChangeType<T>(object value, T defalutValue)
        {
            if (value == null)
            {
                return defalutValue;
            }
            Type nullableType = typeof(T);
            TypeInfo nullableTypeInfo = nullableType.GetTypeInfo();
            if (nullableTypeInfo.IsClass || nullableTypeInfo.IsInterface && nullableType != typeof(string))
            {
                if (value is T)
                {
                    return (T)value;
                }
                return defalutValue;
            }
            if (nullableTypeInfo.IsGenericType && (nullableType.GetGenericTypeDefinition() == typeof(Nullable<>)))
            {
                return (T)Convert.ChangeType(value, Nullable.GetUnderlyingType(nullableType));
            }
            if (nullableTypeInfo.IsEnum)
            {
                return (T)Enum.Parse(nullableType, value.ToString());
            }
            return (T)Convert.ChangeType(value, nullableType);
        }
    }
}
