using System;
using System.Collections.Generic;
using System.Text;

namespace SexyColor.CommonComponents
{
    /// <summary>
    /// 表示不过滤敏感词
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    public class NoFilterWordAttribute : Attribute
    {

    }
}
