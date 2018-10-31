using System.ComponentModel.DataAnnotations;

namespace SexyColor.BusinessComponents
{
    public enum IsNewAtionEnum
    {
        /// <summary>
        /// 否
        /// </summary>
        [Display(Name = "否")]
        No = 0,


        /// <summary>
        /// 是
        /// </summary>
        [Display(Name = "是")]
        Yes = 1,
 
    }

    public enum IsEnableEnum
    {
        /// <summary>
        /// 禁用
        /// </summary>
        [Display(Name = "禁用")]
        No = 0,


        /// <summary>
        /// 启用
        /// </summary>
        [Display(Name = "启用")]
        Yes = 1,
    }


    public enum HeadImageEnum
    {
        /// <summary>
        /// 原始头像
        /// </summary>
        Original,
        /// <summary>
        /// 缩放100*100
        /// </summary>
        Original_100,
        /// <summary>
        /// 缩放50*50
        /// </summary>
        Original_50,
        /// <summary>
        /// 缩放30*30
        /// </summary>
        Original_30,
    }
}
