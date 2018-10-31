using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SexyColor.CommonComponents
{
    /// <summary>
    /// 是否金额格式 0.00
    /// </summary>
    public class IsMoneyType : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null && !string.IsNullOrWhiteSpace(value.ToString()))
            {
                var isBool = Regex.IsMatch(value.ToString(), @"^(([1-9]|[1-9]|(0[.])|((0[.])))[0-9]{0,}(([.]*\d{1,2})|[0-9]{0,})|0)$");
                return isBool;
            }
            else
            {
                return true;
            }
        }
    }
}
