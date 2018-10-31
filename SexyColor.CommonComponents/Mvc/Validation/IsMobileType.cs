using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SexyColor.CommonComponents
{
    /// <summary>
    /// 验证是否是手机号码格式
    /// </summary>
    public  class IsMobileType : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                if (value.ToString().Trim() != "")
                {
                    var isBool = Regex.IsMatch(value.ToString(), "^([1][3|4|5|7|8][0-9]{9})?$");
                    return isBool;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }
    }
}
