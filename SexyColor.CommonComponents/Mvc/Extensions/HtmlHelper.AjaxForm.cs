using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;

namespace SexyColor.CommonComponents
{
    public static class HtmlHelperAjaxFormExtensions
    {
        public static MvcForm BeginAjaxForm(this IHtmlHelper htmlHelper, AjaxFormOptions options)
        {
            return FormHelper(htmlHelper, null , FormMethod.Post, options, new RouteValueDictionary());
        }

        public static MvcForm BeginAjaxForm(this IHtmlHelper htmlHelper, string actionName, string controllerName, FormMethod method, AjaxFormOptions options)
        {
            return BeginAjaxForm(htmlHelper, actionName, controllerName, null, method, options, null);
        }

        public static MvcForm BeginAjaxForm(this IHtmlHelper htmlHelper, string actionName, string controllerName, RouteValueDictionary routeValues, FormMethod method, AjaxFormOptions options)
        {
            return BeginAjaxForm(htmlHelper, actionName, controllerName, routeValues, method, options, null);
        }

        public static MvcForm BeginAjaxForm(this IHtmlHelper htmlHelper, string actionName, string controllerName, object routeValues, FormMethod method, AjaxFormOptions options, object htmlAttributes)
        {
            RouteValueDictionary newValues = new RouteValueDictionary(routeValues);

            Type t = htmlAttributes.GetType();
            PropertyInfo[] proArr = t.GetTypeInfo().GetProperties();
            RouteValueDictionary newAttributes = new RouteValueDictionary();
            foreach (PropertyInfo info in proArr)
            {
                newAttributes.Add(info.Name, info.GetValue(htmlAttributes).ToString());
            }

            return BeginAjaxForm(htmlHelper, actionName, controllerName, newValues, method, options, newAttributes);
        }

        public static MvcForm BeginAjaxForm(this IHtmlHelper htmlHelper, string actionName, string controllerName, RouteValueDictionary routeValues, FormMethod method, AjaxFormOptions options, IDictionary<string, object> htmlAttributes)
        {
            var actionContext = new ActionContext(htmlHelper.ViewContext.HttpContext, htmlHelper.ViewContext.HttpContext.GetRouteData(), new ActionDescriptor());
            string formAction = UrlHelperExtensions.Action(new UrlHelper(actionContext), actionName, controllerName, routeValues ?? new RouteValueDictionary());
            return FormHelper(htmlHelper, formAction, method, options, htmlAttributes);
        }

        private static MvcForm FormHelper(this IHtmlHelper htmlHelper, string formAction, FormMethod method, AjaxFormOptions options, IDictionary<string, object> htmlAttributes)
        {
            TagBuilder builder = new TagBuilder("form");
            builder.MergeAttributes(htmlAttributes);
            if (!string.IsNullOrEmpty(formAction))
                builder.MergeAttribute("action", formAction);
            builder.MergeAttribute("method", GetFormMethodString(method), true);
            builder.MergeAttributes(options.ToHtmlAttributes());

            builder.TagRenderMode = TagRenderMode.StartTag;

            var writer = new StringWriter();
            builder.WriteTo(writer, HtmlEncoder.Default);
            htmlHelper.AntiForgeryToken().WriteTo(writer, HtmlEncoder.Default);
            htmlHelper.ViewContext.Writer.Write(writer.ToString());
            MvcForm theForm = new MvcForm(htmlHelper.ViewContext, HtmlEncoder.Default);

            return theForm;
        }

        private static string GetFormMethodString(FormMethod method)
        {
            StringBuilder builder = new StringBuilder();
            if (method == FormMethod.Post)
                builder.Append("post");
            else
                builder.Append("get");
            return builder.ToString();
        }
    }
}
