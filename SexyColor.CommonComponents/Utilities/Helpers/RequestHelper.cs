using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Net;
using System.Reflection;
using System.Linq;
using System.Text.RegularExpressions;

namespace SexyColor.CommonComponents
{
    public class RequestHelper
    {
        /// <summary>
        /// 获取请求参数 ?后的参数
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static NameValueCollection GetQueryParams(HttpRequest query)
        {
            var url = $"{query.Scheme}://{query.Host}{query.Path}{query.QueryString}";
            int startIndex = url.IndexOf("?");
            NameValueCollection values = new NameValueCollection();
            if (startIndex <= 0)
                return values;
            string[] nameValues = url.Substring(startIndex + 1).Split('&');
            foreach (string s in nameValues)
            {
                string[] pair = s.Split('=');

                string name = pair[0];
                string value = string.Empty;

                if (pair.Length > 1)
                    value = pair[1];

                values.Add(name, WebUtility.UrlDecode(value));
            }
            return values;
        }

        /// <summary>
        /// 获取完整路径
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static string RawUrl(HttpRequest query)
        {
            var url = $"{query.Scheme}://{query.Host}{query.Path}{query.QueryString}";
            return url;
        }

        /// <summary>
        /// 拼接Url参数
        /// </summary>
        /// <param name="query"></param>
        /// <param name="queryKey"></param>
        /// <returns></returns>
        public static string ConnectUrl(HttpRequest query, string queryKey, string queryValue)
        {
            var url = $"{query.Path}{query.QueryString}";
            if (url.IndexOf('?') > 0)
            {
                if (url.IndexOf(queryKey) == -1)
                    url = url + string.Format("&{0}{1}", queryKey, queryValue);
                else
                    url = Regex.Replace(url, string.Format(@"{0}[\s\S]*", queryKey), queryKey + queryValue, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            }
            else
                url = url + string.Format("?{0}{1}", queryKey, queryValue);
            return url; 
        }
    }

    public static class RequestExtension
    {
        public static int GetInt(this HttpRequest request, string key, int defaultValue)
        {
            int value = defaultValue;
            if (request.Form != null)
            {
                IEnumerator<KeyValuePair<string, StringValues>> forms = request.Form.GetEnumerator();
                while (forms.MoveNext())
                {
                    KeyValuePair<string, StringValues> pair = forms.Current;
                    if (pair.Key.Equals(key))
                    {
                        if (int.TryParse(pair.Value, out value))
                            return value;
                    }
                }
            }
            return value;
        }

        public static float GetFloat(this HttpRequest request, string key, float defaultValue)
        {
            float value = defaultValue;
            if (request.Form != null)
            {
                IEnumerator<KeyValuePair<string, StringValues>> forms = request.Form.GetEnumerator();
                while (forms.MoveNext())
                {
                    KeyValuePair<string, StringValues> pair = forms.Current;
                    if (pair.Key.Equals(key))
                    {
                        if (float.TryParse(pair.Value, out value))
                            return value;
                    }
                }
            }
            return value;
        }

        public static bool GetBool(this HttpRequest request, string key, bool defaultValue)
        {
            bool value = defaultValue;
            if (request.Form != null)
            {
                IEnumerator<KeyValuePair<string, StringValues>> forms = request.Form.GetEnumerator();
                while (forms.MoveNext())
                {
                    KeyValuePair<string, StringValues> pair = forms.Current;
                    if (pair.Key.Equals(key))
                    {
                        if (bool.TryParse(pair.Value, out value))
                            return value;
                    }
                }
            }
            return value;
        }

        public static string GetString(this HttpRequest request, string key, string defaultValue)
        {
            string value = defaultValue;
            try
            {
                if (request.Form != null)
                {
                    IEnumerator<KeyValuePair<string, StringValues>> forms = request.Form.GetEnumerator();
                    while (forms.MoveNext())
                    {
                        KeyValuePair<string, StringValues> pair = forms.Current;
                        if (pair.Key.Equals(key))
                        {
                            value = pair.Value;
                            return value;
                        }
                    }
                }
            }
            catch {
                
            }

            return value;
        }

        public static T Get<T>(this HttpRequest request, string key)
        {
            if (typeof(T) == typeof(string))
                return Get<T>(request, key, (T)Convert.ChangeType(string.Empty, typeof(T)));
            else
                return Get<T>(request, key, default(T));
        }

        public static T Get<T>(this HttpRequest request, string key, T defaultValue)
        {
            T value = defaultValue;
            if (request.Form != null)
            {
                IEnumerator<KeyValuePair<string, StringValues>> forms = request.Form.GetEnumerator();
                Type type = typeof(T);
                TypeInfo typeInfo = type.GetTypeInfo();
                if (typeInfo.IsGenericType && typeInfo.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    while (forms.MoveNext())
                    {
                        KeyValuePair<string, StringValues> pair = forms.Current;
                        if (pair.Key.Equals(key))
                        {
                            return (T)TypeDescriptor.GetConverter(Nullable.GetUnderlyingType(type)).ConvertFrom(pair.Value.ToString());
                        }
                    } 
                }
                else if (typeInfo.IsEnum)
                {
                    while (forms.MoveNext())
                    {
                        KeyValuePair<string, StringValues> pair = forms.Current;
                        if (pair.Key.Equals(key))
                        {
                            return (T)Enum.Parse(type, pair.Value.ToString());
                        }
                    }
                }
                else
                {
                    try
                    {
                        while (forms.MoveNext())
                        {
                            KeyValuePair<string, StringValues> pair = forms.Current;
                            if (pair.Key.Equals(key))
                            {
                                return (T)Convert.ChangeType(pair.Value.ToString(), type);
                            }
                        }
                    }
                    catch
                    {
                        return value;
                    }
                }
            }
            return value;
        }

        public static IEnumerable<T> Gets<T>(this HttpRequest request, string key)
        {
            return Gets<T>(request, key, default(IEnumerable<T>));
        }

        public static IEnumerable<T> Gets<T>(this HttpRequest request, string key, IEnumerable<T> defaultValue)
        {
            try
            {
                if (request.Form != null)
                {
                    IEnumerator<KeyValuePair<string, StringValues>> forms = request.Form.GetEnumerator();
                    while (forms.MoveNext())
                    {
                        KeyValuePair<string, StringValues> pair = forms.Current;
                        if (pair.Key.Equals(key))
                        {
                            List<T> list = new List<T>();
                            string[] valArray = pair.Value.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string val in valArray)
                            {
                                try
                                {
                                    list.Add((T)Convert.ChangeType(val, typeof(T)));
                                }
                                catch {
                                }
                            }
                            return list;
                        }
                    }
                }
            }
            catch {

            }
            return defaultValue;
        }
    }
}
