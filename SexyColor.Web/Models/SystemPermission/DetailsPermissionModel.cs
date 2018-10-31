using SexyColor.BusinessComponents;
using System;

namespace SexyColor.Web
{
    public class DetailsPermissionModel
    {
        /// <summary>
        /// 权限项
        /// </summary>
        public string Itemkey { get; set; }

        /// <summary>
        /// 应用Id 
        /// </summary>
        public int Applicationid { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string Itemname { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 权限父级
        /// </summary>
        public string Parentsid { get; set; }

        /// <summary>
        /// 权限父级名称
        /// </summary>
        public string ParentsName { get; set; }

        /// <summary>
        /// 权限Url
        /// </summary>
        public string ItemUrl { get; set; }

        /// <summary>
        /// 事件或者按钮名
        /// </summary>
        public string EventName { get; set; }

        /// <summary>
        /// 权限类型
        /// </summary>
        public int ItemType { get; set; }

        /// <summary>
        /// 是否新功能
        /// </summary>
        public bool IsNewAction { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }


        /// <summary>
        /// 将PermissionItems转换DetailsPermissionModel 采用链式
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public DetailsPermissionModel ToDetailsPermissionModel(PermissionItems item)
        {
            Itemkey = item.Itemkey;
            Applicationid = item.Applicationid;
            Itemname = item.Itemname;
            Parentsid = item.Parentsid;
            IsEnable = item.IsEnable;
            DateCreated = item.DateCreated;
            DisplayOrder = item.DisplayOrder;
            ItemType = item.ItemType;
            IsNewAction = item.IsNewAction;
            EventName = item.EventName;
            ItemUrl = item.ItemUrl;
            return this;
        }
    }
}
