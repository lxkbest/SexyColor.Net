using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SexyColor.CommonComponents
{
    /// <summary>
    /// 是否是数字格式
    /// </summary>
    public class IsPositiveIntegerType : ValidationAttribute, IClientModelValidator
    {
        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-IsPositiveIntegerType", ErrorMessage);
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                if (Regex.IsMatch(value.ToString(), "^[0-9]*$")) return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage);
        }
    }
}
