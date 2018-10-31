using System;
using System.Collections.Generic;
using System.Text;

namespace SexyColor.Infrastructure
{
    public enum CachingExpirationType
    {

        /// <summary>
        /// 永久不变的
        /// </summary>
        Invariable = 0,

        /// <summary>
        /// 稳定数据   例如： Resources.xml/Area/School
        /// </summary>
        Stable = 1,

        /// <summary>
        /// 相对稳定    例如：权限配置、审核配置
        /// </summary>
        RelativelyStable = 2,
 
        /// <summary>
        /// 常用的单个对象 例如： 用户、商品
        /// </summary>
        UsualSingleObject = 3,

        /// <summary>
        /// 常用的对象集合 例如： 用户的朋友
        /// </summary>
        UsualObjectCollection = 4,

        /// <summary>
        /// 单个对象    例如： 圈子、帖子
        /// </summary>
        SingleObject = 5,

        /// <summary>
        /// 对象集合    例如： 订单，订单行
        /// </summary>
        ObjectCollection = 6
    }
}
