using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SexyColor.BusinessComponents;
using SexyColor.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SexyColor.CommonComponents
{
    public class Utility
    {
        /// <summary>
        /// 验证邮箱密码
        /// </summary>
        /// <param name="newPassword">密码</param>
        /// <param name="errorMessage">提示信息</param>
        /// <returns></returns>
        public static bool ValidatePassword(string newPassword, out string errorMessage)
        {
            int minRequiredPasswordLength = 6;
            int minRequiredNonAlphanumericCharacters = 2;

            errorMessage = "";

            if (string.IsNullOrEmpty(newPassword))
            {
                errorMessage = "密码不能为空";
                return false;
            }

            if (newPassword.Length < minRequiredPasswordLength)
            {
                errorMessage = string.Format("密码长度不能少于{0}个字符", minRequiredPasswordLength);
                return false;
            }

            int nonAlphaNumChars = 0;
            for (int i = 0; i < newPassword.Length; i++)
            {
                if (!char.IsLetterOrDigit(newPassword, i))
                    nonAlphaNumChars++;
            }
            if (nonAlphaNumChars < minRequiredNonAlphanumericCharacters)
            {
                errorMessage = string.Format("您的密码中特殊字符太少，请至少输入 {0} 个特殊字符（如：*、$等）", minRequiredNonAlphanumericCharacters);
                return false;
            }

            return true;
        }


        /// <summary>
        /// 验证邮箱
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="errorMessage">提示信息</param>
        /// <returns></returns>
        public static bool ValidateEmail(string email, out string errorMessage)
        {
            if (string.IsNullOrEmpty(email))
            {
                errorMessage = "请输入邮箱";
                return false;
            }
            string emailRegex = @"^([a-zA-Z0-9_\.-])+@([a-zA-Z0-9_-])+((\.[a-zA-Z0-9_-]{2,3}){1,2})$";
            Regex regex = new Regex(emailRegex, RegexOptions.ECMAScript);
            if (!regex.IsMatch(email))
            {
                errorMessage = "邮箱格式不正确，请输入正确格式的邮箱地址";
                return false;
            }

            IUserService userService = DIContainer.Resolve<IUserService>();
            User user = userService.FindUserByEmail(email);
            if (user != null)
            {
                errorMessage = string.Format("该邮箱已被占用，请更换邮箱重试");
                return false;
            }
            errorMessage = string.Empty;
            return true;
        }

        /// <summary>
        /// 验证账号
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static bool ValidateUserName(string userName, out string errorMessage)
        {
            int minUserNameLength = 2;
            int maxUserNameLength = 64;
            if (string.IsNullOrEmpty(userName))
            {
                errorMessage = "请输入账号";
                return false;
            }

            if (userName.Contains("*"))
            {

                errorMessage = "非法账号";
                return false;
            }

            if (userName.Length < minUserNameLength)
            {
                errorMessage = string.Format("账号长度不能少于{0}位", minUserNameLength);
                return false;
            }

            if (userName.Length > maxUserNameLength)
            {

                errorMessage = string.Format("账号长度最多{0}位", maxUserNameLength);
                return false;
            }

            string nickNameRegex = @"^[\w|\u4e00-\u9fa5]{1,64}$";
            Regex regex = new Regex(nickNameRegex);
            if (!regex.IsMatch(userName))
            {
                errorMessage = "只能输入字母、数字、汉字和下划线";
                return false;
            }


            IUserService userService = DIContainer.Resolve<IUserService>();
            User user = userService.GetUser(userName);
            if (user != null)
            {
                errorMessage = "该账号已被占用，请更换账号重试";
                return false;
            }
            errorMessage = string.Empty;
            return true;
        }

        public static bool ValidateNickName(string nickName, out string errorMessage, long? userId = null)
        {

            int minUserNameLength = 2;
            int maxUserNameLength = 64;

            if (string.IsNullOrEmpty(nickName))
            {
                errorMessage = "请输入昵称";
                return false;
            }

            if (nickName.Contains("*"))
            {
                errorMessage = "非法昵称";
                return false;
            }

            if (nickName.Length < minUserNameLength)
            {
                errorMessage = string.Format("昵称长度不能少于{0}位", minUserNameLength);
                return false;
            }

            if (nickName.Length > maxUserNameLength)
            {
                errorMessage = string.Format("昵称长度最多{0}位", maxUserNameLength);
                return false;
            }

            string nickNameRegex = @"^[\w|\u4e00-\u9fa5]{1,64}$";
            Regex regex = new Regex(nickNameRegex);
            if (!regex.IsMatch(nickName))
            {
                errorMessage = "只能输入字母、数字、汉字和下划线";
                return false;
            }


            IUserService userService = DIContainer.Resolve<IUserService>();
            User user = userService.GetUserByNickName(nickName);
            if (user != null)
            {
                if (userId.HasValue)
                {
                    if (user.UserId == userId.Value)
                    {
                        errorMessage = string.Empty;
                        return true;
                    }
                }
                errorMessage = "该昵称已被占用，请更换其他昵称重试";
                return false;
            }
            errorMessage = string.Empty;
            return true;
        }

        /// <summary>
        /// 用户角色-验证角色名称
        /// </summary>
        /// <param name="userName">角色名称</param>
        /// <returns></returns>
        public static bool ValidateRolesName(string roleName, out string errorMessage)
        {
            RolesService rolesService = DIContainer.Resolve<RolesService>();
            Roles role = rolesService.GetFullRoles(roleName);
            if (role != null)
            {
                if (role.Rolename != roleName)
                {
                    errorMessage = string.Empty;
                    return true;
                }
                errorMessage = "该名称已被占用，请更换其他名称重试";
                return false;
            }
            errorMessage = string.Empty;
            return true;
        }

        /// <summary>
        /// 获取用户IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetIP()
        {
            string result = (HttpContextCore.Current.Request.Headers["HTTP_X_FORWARDED_FOR"].ToString() != null
            && HttpContextCore.Current.Request.Headers["HTTP_X_FORWARDED_FOR"] != string.Empty)
            ? HttpContextCore.Current.Request.Headers["HTTP_X_FORWARDED_FOR"]
            : HttpContextCore.Current.Request.Headers["REMOTE_ADDR"];

            if (string.IsNullOrEmpty(result))
                result = HttpContextCore.Current.Request.Headers["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(result))
                return "127.0.0.1";

            return result;

        }

        /// <summary>
        /// 字符串转枚举类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T StringConvertToEnum<T>(string str) where T : struct
        {
            T t = default(T);
            return Enum.TryParse(str, true, out t) ? t : default(T);
        }

        /// <summary>
        /// 遍历枚举获取字典
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Dictionary<int, string> GetDictionaryByEnumMemberInfo(Type type)
        {
            var dic = new Dictionary<int, string>();
            var typeInfo = type.GetTypeInfo();
            var enumValues = typeInfo.GetEnumValues();
            foreach (var value in enumValues)
            {
                MemberInfo memberInfo = typeInfo.GetMember(value.ToString()).First();
                var displayAttribute = memberInfo.GetCustomAttribute<DisplayAttribute>();
                dic.Add((int)value, displayAttribute.Name);
            }
            return dic;
        }

        /// <summary>
        /// 获取枚举值的显示属性
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDisplayByEnumMemberInfo(Type type, object value)
        {
            try
            {
                var typeInfo = type.GetTypeInfo();
                MemberInfo memberInfo = typeInfo.GetMember(value.ToString()).First();
                var displayAttribute = memberInfo.GetCustomAttribute<DisplayAttribute>();
                return displayAttribute.Name;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static string CutString(string str, int startIndex)
        {
            return CutString(str, startIndex, str.Length);
        }

        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string CutString(string str, int startIndex, int length)
        {
            if (startIndex >= 0)
            {
                if (length < 0)
                {
                    length = length * -1;
                    if (startIndex - length < 0)
                    {
                        length = startIndex;
                        startIndex = 0;
                    }
                    else
                        startIndex = startIndex - length;
                }

                if (startIndex > str.Length)
                    return "";
            }
            else
            {
                if (length < 0)
                    return "";
                else
                {
                    if (length + startIndex > 0)
                    {
                        length = length + startIndex;
                        startIndex = 0;
                    }
                    else
                        return "";
                }
            }

            if (str.Length - startIndex < length)
                length = str.Length - startIndex;

            return str.Substring(startIndex, length);
        }

        /// <summary>
        /// 截取字符串到指定位置并使用其他字符代替超出部分显示
        /// </summary>
        /// <returns></returns>
        public static string GetSubString(string srcString, int startIndex, int length, string tailString)
        {
            string myResult = srcString;
            Byte[] bComments = Encoding.UTF8.GetBytes(srcString);
            foreach (char c in Encoding.UTF8.GetChars(bComments))
            {    //当是日文或韩文时(注:中文的范围:\u4e00 - \u9fa5, 日文在\u0800 - \u4e00, 韩文为\xAC00-\xD7A3)
                if ((c > '\u0800' && c < '\u4e00') || (c > '\xAC00' && c < '\xD7A3'))
                {
                    if (startIndex >= srcString.Length)
                        return "";
                    else
                        return srcString.Substring(startIndex,
                                                       ((length + startIndex) > srcString.Length) ? (srcString.Length - startIndex) : length);
                }
            }
            if (length >= 0)
            {
                byte[] bsSrcString = Encoding.UTF8.GetBytes(srcString);

                //当字符串长度大于起始位置
                if (bsSrcString.Length > startIndex)
                {
                    int p_EndIndex = bsSrcString.Length;

                    //当要截取的长度在字符串的有效长度范围内
                    if (bsSrcString.Length > (startIndex + length))
                    {
                        p_EndIndex = length + startIndex;
                    }
                    else
                    {   //当不在有效范围内时,只取到字符串的结尾
                        length = bsSrcString.Length - startIndex;
                        tailString = "";
                    }
                    int nRealLength = length;
                    int[] anResultFlag = new int[length];
                    byte[] bsResult = null;
                    int nFlag = 0;
                    for (int i = startIndex; i < p_EndIndex; i++)
                    {
                        if (bsSrcString[i] > 127)
                        {
                            nFlag++;
                            if (nFlag == 3)
                                nFlag = 1;
                        }
                        else
                            nFlag = 0;
                        anResultFlag[i] = nFlag;
                    }
                    if ((bsSrcString[p_EndIndex - 1] > 127) && (anResultFlag[length - 1] == 1))
                        nRealLength = length + 1;
                    bsResult = new byte[nRealLength];
                    Array.Copy(bsSrcString, startIndex, bsResult, 0, nRealLength);
                    myResult = Encoding.UTF8.GetString(bsResult);
                    myResult = myResult + tailString;
                }
            }
            return myResult;
        }

        /// <summary>
        /// 调整图片
        /// </summary>
        /// <returns></returns>
        public static Bitmap ResizeImage(Bitmap bmp, int newW, int newH)
        {
            try
            {
                Bitmap b = new Bitmap(newW, newH);
                Graphics g = Graphics.FromImage(b);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                g.Dispose();
                return b;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 裁剪图片
        /// </summary>
        /// <returns></returns>
        public static Bitmap CutImage(Bitmap bitmap, int srcX, int srcY, int width, int height)
        {
            if (bitmap == null)
                return null;
            int w = bitmap.Width;
            int h = bitmap.Height;
            if (srcX >= w || srcY >= h)
                return null;
            if (srcX + width > w)
                width = w - srcX;
            if (srcY + height > h)
                height = h - srcY;
            try
            {
                Bitmap bmpOut = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                Graphics g = Graphics.FromImage(bmpOut);
                g.DrawImage(bitmap, new Rectangle(0, 0, width, height), new Rectangle(srcX, srcY, width, height), GraphicsUnit.Pixel);
                g.Dispose();
                return bmpOut;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="env"></param>
        /// <param name="file"></param>
        /// <param name="req"></param>
        /// <param name="saveSuffix"></param>
        /// <returns></returns>
        public static async Task<string> SaveImage(IHostingEnvironment env, IFormFile file, HttpRequest req, string saveSuffix)
        {
            var filePath = string.Format("/Uploads/Images/{0}/{1}/{2}/", DateTime.Now.ToString("yyyy"), DateTime.Now.ToString("MM"), DateTime.Now.ToString("dd"));
            var qiniuFilePath = string.Format("{0}/{1}/{2}/", DateTime.Now.ToString("yyyy"), DateTime.Now.ToString("MM"), DateTime.Now.ToString("dd"));
            string imageUrl = string.Empty;
            if (!Directory.Exists(env.WebRootPath + filePath))
                Directory.CreateDirectory(env.WebRootPath + filePath);

            var strDateTime = DateTime.Now.ToString("yyyyMMddhhmmssfff");
            var strRan = Convert.ToString(new Random().Next(100, 999));
            var saveName = strDateTime + strRan;
            var useQiNiu = BuilderContext.Configuration["UseQiNiu"];
            var qiniuHost = BuilderContext.Configuration["QiNiuHost"];
            var pathName = string.Empty;

            string hostPath = WebSiteUtility.MapPhysicsToUrl($"{filePath}{saveName}");
            string qiniuHostPath = WebSiteUtility.MapPhysicsToUrl($"{qiniuHost}{qiniuFilePath}{saveName}");

            using (var stream = new FileStream($"{env.WebRootPath}{filePath}{saveName}{saveSuffix}", FileMode.Create))
            {
                await file.CopyToAsync(stream);
                stream.Flush();
                pathName = stream.Name;
                imageUrl = $"{req.Scheme}://{req.Host}{hostPath}{saveSuffix}";
            }
            if (useQiNiu.Equals("true", StringComparison.CurrentCultureIgnoreCase))
            {
                await Task.Run(() =>
                {
                    QiniuManager qiniu = new QiniuManager();
                    string savekey = $"{qiniuFilePath}{saveName}";
                    byte[] data = System.IO.File.ReadAllBytes(pathName);
                    qiniu.ByteUpload(data, savekey);
                });
                imageUrl = qiniuHostPath;
            }
            return imageUrl.IsNotNullAndWhiteSpace() ? imageUrl : null;
        }

        /// <summary>
        /// 生成订单号
        /// </summary>
        /// <returns></returns>
        public static string GenerateOrderNumber(string machineCode, string userCode)
        {
            var handleCode = "";
            var hashCode = Guid.NewGuid().GetHashCode();
            if (hashCode < 0)
                hashCode = -hashCode;
            handleCode = hashCode.ToString();
            while (handleCode.Length < 8)
            {
                handleCode = $"{handleCode}{0}";
            }

            if (handleCode.Length > 8)
            {
                handleCode = handleCode.Substring(0, 8);
            }
            return $"{handleCode}{machineCode}{userCode}";
        }


        public static string HandleImage(string imageUrl, HeadImageEnum imageEnum)
        {
            var host =  BuilderContext.Configuration["QiNiuHost"];
            if (imageUrl.IndexOf(host) != -1)
            {
                switch (imageEnum)
                {
                    case HeadImageEnum.Original:
                        break;
                    case HeadImageEnum.Original_100:
                        imageUrl = imageUrl + "_100x100";
                        break;
                    case HeadImageEnum.Original_50:
                        imageUrl = imageUrl + "_50x50";
                        break;
                    case HeadImageEnum.Original_30:
                        imageUrl = imageUrl + "_30x30";
                        break;
                }
            }
            else
            {
                var splitArr = imageUrl.Split('.');
                if (splitArr.Length > 0)
                {
                    switch (imageEnum)
                    {
                        case HeadImageEnum.Original:
                            break;
                        case HeadImageEnum.Original_100:
                            imageUrl = splitArr[0] + "_100x100." + splitArr[1];
                            break;
                        case HeadImageEnum.Original_50:
                            imageUrl = splitArr[0] + "_50x50." + splitArr[1];
                            break;
                        case HeadImageEnum.Original_30:
                            imageUrl = splitArr[0] + "_30x30." + splitArr[1];
                            break;
                    }
                }
                
            }

            return imageUrl;
        }
    }
 

    
    
}
