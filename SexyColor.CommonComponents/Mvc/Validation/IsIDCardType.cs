using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SexyColor.CommonComponents.Mvc.Validation
{
    /// <summary>
    /// 验证是否是身份证格式
    /// </summary>
    public class IsIDCardType : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                if (value.ToString().Trim() != "")
                {
                    return Regex.IsMatch(value.ToString(), "^[1-9]([0-9]{16}|[0-9]{13})[xX0-9]$");
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
