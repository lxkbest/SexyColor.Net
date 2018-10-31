using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using System;
using System.IO;
using System.Reflection;

using System.Text.Encodings.Web;

namespace SexyColor.CommonComponents
{
    public static class HtmlHelperCommonExtensions
    {
        public static IHtmlContent Icon(this IHtmlHelper html)
        {
            return HtmlString.Empty;
        }

        public static IHtmlContent CustomCheckBox(this IHtmlHelper htmlhelper, string name, object value, bool isChecked = false, object htmlAttributes = null)
        {
            TagBuilder builder = new TagBuilder("input");
            builder.MergeAttribute("type", "checkbox");
            builder.MergeAttribute("name", name);
            if (value != null)
                builder.MergeAttribute("value", value.ToString());
            if (isChecked)
                builder.MergeAttribute("checked", "checked");
            if (htmlAttributes != null)
            {
                Type t = htmlAttributes.GetType();
                PropertyInfo[] proArr = t.GetTypeInfo().GetProperties();
                RouteValueDictionary attributes = new RouteValueDictionary();
                foreach (PropertyInfo info in proArr)
                {
                    attributes.Add(info.Name, info.GetValue(htmlAttributes).ToString());
                }
                builder.MergeAttributes(attributes, false);
            }
            var writer = new StringWriter();
            builder.WriteTo(writer, HtmlEncoder.Default);
            return new HtmlString(writer.ToString());
        }

    }
}
