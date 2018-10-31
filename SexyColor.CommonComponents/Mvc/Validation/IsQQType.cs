using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SexyColor.CommonComponents
{
    /// <summary>
    /// 是否是QQ号码格式
    /// </summary>
    public class IsQQType : ValidationAttribute
    {
        //public override bool IsValid(object value)
        //{
        //    if (value != null)
        //    {
        //        return Regex.IsMatch(value.ToString(), "[1-9][0-9]{4,12}");
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var result = Regex.IsMatch(value.ToString(), "[1-9][0-9]{4,12}");
                if (result)
                    return ValidationResult.Success;
                else
                    return new ValidationResult(this.ErrorMessage);
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }

}
