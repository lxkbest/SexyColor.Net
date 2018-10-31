using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SexyColor.CommonComponents.Mvc.Validation
{
    /// <summary>
    /// 验证是否是日期格式 yyyy-mm-dd
    /// </summary>
    public class IsDateTimeType : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                if (value.ToString().Trim() != "")
                {
                    return Regex.IsMatch(value.ToString(), "/^(d{2}|d{4})-((0([1-9]{1}))|(1[1|2]))-(([0-2]([1-9]{1}))|(3[0|1]))$/");
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
