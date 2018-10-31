using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SexyColor.CommonComponents
{
    /// <summary>
    /// 是否是电话号码格式
    /// </summary>
    public class IsTelType : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                if (value.ToString().Trim() != "")
                {
                    return Regex.IsMatch(value.ToString(), "^[[0-9]{3}-|[0-9]{4}-]?([0-9]{8}|[0-9]{7})?$");
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
