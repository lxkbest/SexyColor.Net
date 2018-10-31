using System;
using System.Collections.Generic;
using System.Text;

namespace SexyColor.BusinessComponents
{
    public class OperationLogsQuery
    {
        /// <summary>
        /// 所属应用
        /// </summary>
        public int? ApplicationId = null;
        /// <summary>
        /// 类型 功能或者菜单
        /// </summary>
        public string OperationType = string.Empty;
        /// <summary>
        /// 操作功能名称
        /// </summary>
        public string OperationName = string.Empty;
        /// <summary>
        /// 创建时间下限
        /// </summary>
        public DateTime? TimeLowerLimit = null;
        /// <summary>
        /// 创建时间上限
        /// </summary>
        public DateTime? TimeUpperLimit = null;
        /// <summary>
        /// 操作人
        /// </summary>
        public string Username = string.Empty;
        /// <summary>
        /// 排序
        /// </summary>
        public OperationLogsSortBy? OperationLogsSortBy = BusinessComponents.OperationLogsSortBy.DateCreated_Desc;
    }

    public enum OperationLogsSortBy
    {
        Id = 1,
        Id_Desc = 2,
        DateCreated = 3,
        DateCreated_Desc = 4
    }
}
