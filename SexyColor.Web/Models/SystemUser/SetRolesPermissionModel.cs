using SexyColor.BusinessComponents;
using System;
using System.Collections.Generic;

namespace SexyColor.Web
{
    public class SetRolesPermissionModel
    {
        /// <summary>
        /// 权限key
        /// </summary>
        public string Itemkey { get; set; }

        /// <summary>
        /// 模块Id   (后期使用)
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
        /// 上次更新时间
        /// </summary>
        public DateTime DateLastModified { get; set; }

        /// <summary>
        /// 是否拥有此权限项
        /// </summary>
        public bool IsHave{ get; set; }

        public SetRolesPermissionModel ToSetRolesPermissionModel(PermissionItems item)
        {
            Itemkey = item.Itemkey;
            Applicationid = item.Applicationid;
            Itemname = item.Itemname;
            Parentsid = item.Parentsid;
            IsEnable = item.IsEnable;
            DisplayOrder = item.DisplayOrder;
            ItemType = item.ItemType;
            IsNewAction = item.IsNewAction;
            EventName = item.EventName;
            ItemUrl = item.ItemUrl;
            DateCreated = item.DateCreated;
            DateLastModified = item.DateLastModified;
            return this;
        }
    }
}
