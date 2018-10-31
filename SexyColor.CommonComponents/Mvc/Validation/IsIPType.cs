using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace SexyColor.CommonComponents.Mvc.Validation
{
    /// <summary>
    /// 验证是否是IP格式
    /// </summary>
    public class IsIPType : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null && !string.IsNullOrWhiteSpace(value.ToString()))
            {
                return Regex.IsMatch(value.ToString(), "^(d{1,2}|1dd|2[0-4]d|25[0-5]).(d{1,2}|1dd|2[0-4]d|25[0-5]).(d{1,2}|1dd|2[0-4]d|25[0-5]).(d{1,2}|1dd|2[0-4]d|25[0-5])$");
            }
            else
            {
                return true;
            }
        }
    }
}
