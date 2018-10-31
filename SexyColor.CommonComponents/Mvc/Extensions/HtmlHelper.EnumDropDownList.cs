using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace SexyColor.CommonComponents
{
    public static class HtmlHelperEnumDropDownListExtensions
    {
        public static IHtmlContent EnumDropDownList<TEnum>(this IHtmlHelper htmlHelper, string name, TEnum selectValue, string optionLabel = null, string optionValue = null, object htmlAttributes = null)
        {
            IEnumerable<SelectListItem> items = GetSelectListItems<TEnum>(selectValue, optionLabel, optionValue);
            return htmlHelper.DropDownList(name, items, htmlAttributes);
        }

        public static IHtmlContent EnumDropDownListFor<TModel, TRuslt, TEnum>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TRuslt>> expression, TEnum selectValue, string optionLabel = null, string optionValue = null, object htmlAttributes = null)
        {
            //ModelExplorer explorer = ExpressionMetadataProvider.FromLambdaExpression(expression, htmlHelper.ViewData, htmlHelper.MetadataProvider);
            //IEnumerable<SelectListItem> items = GetSelectListItems<TEnum>(explorer.Model, optionLabel, optionValue);
            IEnumerable<SelectListItem> items = GetSelectListItems<TEnum>(selectValue, optionLabel, optionValue);
            return htmlHelper.DropDownListFor(expression, items, htmlAttributes);
        }


        private static IEnumerable<SelectListItem> GetSelectListItems<TEnum>(object selectValue, string optionLabel, string optionValue)
        {
            Type enumType = typeof(TEnum);
            Type underlyingType = Nullable.GetUnderlyingType(enumType);
            if (underlyingType != null)
            {
                enumType = underlyingType;
            }
            IEnumerable<TEnum> values = Enum.GetValues(enumType).Cast<TEnum>();

            foreach (var value in values)
            {
                if (value.GetType().GetTypeInfo().IsEnum)
                {
                    var enumValue = Enum.Parse(value.GetType(), value.ToString());
                    int val = (int)enumValue;
                    var selected = value.Equals(selectValue);
                }
            }


            IEnumerable<SelectListItem> items = from value in values
                                                select new SelectListItem
                                                {
                                                    Text = GetEnumDescription(enumType, value),
                                                    Value = ((int)Enum.Parse(value.GetType(), value.ToString())).ToString(),
                                                    Selected = value.Equals(selectValue)
                                                };

            if (!string.IsNullOrEmpty(optionLabel))
                items = (new List<SelectListItem> { new SelectListItem { Text = optionLabel, Value = string.IsNullOrWhiteSpace(optionValue) ? string
                    .Empty : optionValue } }).Concat(items);
            return items;
        }

        private static string GetEnumDescription<TEnum>(Type enumType, TEnum value)
        {
            //MemberInfo memberInfo = enumType.GetTypeInfo().GetMember(value.ToString()).First();
            //var displayAttribute = memberInfo.GetCustomAttribute<DisplayAttribute>();
            //return displayAttribute.Name;

            FieldInfo fi = value.GetType().GetField(value.ToString());
            var attribute = fi.GetCustomAttributes(
                  typeof(DisplayAttribute), false)
                   .Cast<DisplayAttribute>()
                   .FirstOrDefault();
            if (attribute != null)
                return attribute.Name;
            return value.ToString();
        }
    }
}
