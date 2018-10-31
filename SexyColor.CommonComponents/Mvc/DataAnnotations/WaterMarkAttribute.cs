using System;

namespace SexyColor.CommonComponents
{

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class WaterMarkAttribute : Attribute
    {
        private string _content = string.Empty;
        /// <summary>
        /// 水印文字内容
        /// </summary>
        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }

        /// <summary>
        /// 构造器
        /// </summary>
        public WaterMarkAttribute()
        {
        }
    }
}
