using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text;

namespace SexyColor.CommonComponents
{
    public static class SessionExtension
    {
        /// <summary>
        /// 设置session
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="session">session对象</param>
        /// <param name="key">键</param>
        /// <param name="val">值</param>
        /// <returns></returns>
        public static bool Set<T>(this ISession session, string key, T val)
        {
            if (string.IsNullOrWhiteSpace(key) || val == null)
                return false;
            var strVal = JsonConvert.SerializeObject(val);
            var bb = Encoding.UTF8.GetBytes(strVal);
            session.Set(key, bb);
            return true;
        }

        /// <summary>
        /// 获取session
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="session">session对象</param>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static T Get<T>(this ISession session, string key)
        {
            var t = default(T);
            if (string.IsNullOrWhiteSpace(key))
                return t;
            if (session.TryGetValue(key, out byte[] val))
            {
                var strVal = Encoding.UTF8.GetString(val);
                t = JsonConvert.DeserializeObject<T>(strVal);
            }
            return t;
        }

        public static T Get<T>(this ISession session, string key, T t)
        {
            if (string.IsNullOrWhiteSpace(key))
                return t;
            if (session.TryGetValue(key, out byte[] val))
            {
                var strVal = Encoding.UTF8.GetString(val);
                t = JsonConvert.DeserializeObject<T>(strVal);
            }
            return t;
        }
    }

    
}
