using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using SexyColor.Infrastructure;

namespace SexyColor.CommonComponents
{
    public static class VerificationCodeManager
    {
        private static ICacheService cacheService = DIContainer.Resolve<ICacheService>();
        private static readonly object lockObj = new object();
        private static VerificationCodePersistenceMode persistenceMode = VerificationCodePersistenceMode.Cache;
        private delegate object GetPersistedValueStrategy(HttpContext context, string internalKey, bool remove);
        private static GetPersistedValueStrategy getPersistedValue = GetCacheValue;

        /// <summary>
        /// 在缓存中获取图片流
        /// </summary>
        /// <param name="publicKey">公钥</param>
        /// <returns></returns>
        public static MemoryStream GetCachedImageStream(string publicKey)
        {
            if (string.IsNullOrEmpty(publicKey))
                throw new ArgumentException("Errors.ImageKeyNotSpecified", "publicKey");
            //转换公钥为图片缓存的ImageKey
            string imageKey = GetCacheKeyForImage(publicKey);
            try
            {
                //获取缓存中的二进制数据
                byte[] data = (byte[])cacheService.Get(imageKey);
                //如果二进制为返回Null
                if (data == null)
                    return null;
                MemoryStream stream = new MemoryStream(data);
                return stream;
            }
            catch
            { }
            return null;
        }

        /// <summary>
        /// 生成图片
        /// </summary>
        /// <returns></returns>
        public static string GenerateAndCacheImage(out string publicKey)
        {
            MemoryStream stream;
            string code;
            stream = CreateImage(out code);

            lock (lockObj)
            {
                publicKey = GeneratePublicCacheKey(code);
                string imageKey = GetCacheKeyForImage(publicKey);
                cacheService.Add(imageKey, stream.ToArray(), new TimeSpan(0, 5, 0));
            }
            return code;
        }

        /// <summary>
        /// 缓存验证码
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// <param name="code">验证码</param>
        /// <param name="publicKey">公钥</param>
        public static void CacheCode(HttpContext context, string code, string publicKey)
        {
            string textKey = GetCacheKeyForText(publicKey);
            if (persistenceMode == VerificationCodePersistenceMode.Cache)
                cacheService.Add(textKey, code, new TimeSpan(0, 5, 0));
            else
                context.Session.Set<string>(textKey, code);
        }

        /// <summary>
        /// 公钥转换成验证码图片缓存Key
        /// </summary>
        /// <param name="publicKey">公钥</param>
        /// <returns></returns>
        private static string GetCacheKeyForImage(string publicKey)
        {
            return publicKey + "_image";
        }

        /// <summary>
        /// 公钥转换成验证码文字缓存Key
        /// </summary>
        /// <param name="publicKey">公钥</param>
        /// <returns></returns>
        private static string GetCacheKeyForText(string publicKey)
        {
            return publicKey + "_text";
        }

        /// <summary>
        /// 根据验证码生成公钥
        /// </summary>
        /// <param name="text">验证码</param>
        /// <returns></returns>
        private static string GeneratePublicCacheKey(string code)
        {
            using (SHA1 sha1 = SHA1.Create())
            {
                return Convert.ToBase64String(sha1.ComputeHash(Encoding.UTF8.GetBytes(code + DateTime.Now.Ticks)));
            }
        }

        /// <summary>
        /// 获取验证码的字符（用来验证用户输入的是否正确）并且自动从缓存中移除数据
        /// </summary>
        public static string GetCachedTextAndForceExpire(HttpContext context, string publicKey)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            string imageKey = GetCacheKeyForImage(publicKey);
            string textKey = GetCacheKeyForText(publicKey);

            try
            {
                string text = (string)getPersistedValue(context, textKey, true);
                return text;
            }
            finally
            {
                ExpireCachedImage(imageKey);
            }
        }

        /// <summary>
        /// 释放图像资源
        /// </summary>
        private static void ExpireCachedImage(string internalKey)
        {
            if (!string.IsNullOrEmpty(internalKey))
            {
                try
                {
                    cacheService.Remove(internalKey);
                }
                finally { }
            }
        }

        /// <summary>
        /// 从session中获取值
        /// </summary>
        private static object GetSessionValue(HttpContext context, string key, bool remove)
        {
            if (context.Session == null)
                throw new Exception("SessionStateRequired"); //SessionStateRequiredException();

            try
            {
                return context.Session.Get<object>(key);
            }
            finally
            {
                if (remove)
                    context.Session.Remove(key);
            }
        }

        /// <summary>
        /// 从缓存中获取值
        /// </summary>
        private static object GetCacheValue(HttpContext context, string key, bool remove)
        {
            try
            {
                return cacheService.Get(key);
            }
            finally
            {
                if (remove) cacheService.Remove(key);
            }
        }

        private static string RandomNum(int VcodeNum)
        {
            //验证码可以显示的字符集合
            string Vchar = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,l,m,n,p" +
          ",q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,I,J,K,L,M,N,P,P,Q" +
          ",R,S,T,U,V,W,X,Y,Z";
            string[] VcArray = Vchar.Split(new Char[] { ',' });//拆分成数组
            string code = "";//产生的随机数
            int temp = -1;//记录上次随机数值，尽量避避免生产几个一样的随机数
            Random rand = new Random();
            //采用一个简单的算法以保证生成随机数的不同
            for (int i = 1; i < VcodeNum + 1; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * unchecked((int)DateTime.Now.Ticks));//初始化随机类
                }
                int t = rand.Next(61);//获取随机数
                if (temp != -1 && temp == t)
                {
                    return RandomNum(VcodeNum);//如果获取的随机数重复，则递归调用
                }
                temp = t;//把本次产生的随机数记录起来
                code += VcArray[t];//随机数的位数加一
            }
            return code;
        }


        public static MemoryStream CreateImage(out string code, int numbers = 4)
        {
            code = RandomNum(numbers);
            Bitmap Img = null;
            Graphics g = null;
            MemoryStream ms = null;
            Random random = new Random();
            //验证码颜色集合
            Color[] c = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };
            //验证码字体集合
            string[] fonts = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "宋体" };
            //定义图像的大小，生成图像的实例
            Img = new Bitmap((int)code.Length * 18, 32);
            g = Graphics.FromImage(Img);//从Img对象生成新的Graphics对象
            g.Clear(Color.White);//背景设为白色
                                 //在随机位置画背景点
            for (int i = 0; i < 100; i++)
            {
                int x = random.Next(Img.Width);
                int y = random.Next(Img.Height);
                g.DrawRectangle(new Pen(Color.LightGray, 0), x, y, 1, 1);
            }
            //验证码绘制在g中
            for (int i = 0; i < code.Length; i++)
            {
                int cindex = random.Next(7);//随机颜色索引值
                int findex = random.Next(5);//随机字体索引值
                Font f = new Font(fonts[findex], 15, FontStyle.Bold);//字体
                Brush b = new SolidBrush(c[cindex]);//颜色
                int ii = 4;
                if ((i + 1) % 2 == 0)//控制验证码不在同一高度
                {
                    ii = 2;
                }
                g.DrawString(code.Substring(i, 1), f, b, 3 + (i * 12), ii);//绘制一个验证字符
            }
            ms = new MemoryStream();//生成内存流对象
            Img.Save(ms, ImageFormat.Jpeg);//将此图像以Png图像文件的格式保存到流中
                                           //回收资源
            g.Dispose();
            Img.Dispose();
            return ms;
        }
    }
}
