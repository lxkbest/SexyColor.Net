using MySqlSugar;
using SexyColor.Infrastructure;
using System;

namespace SexyColor.BusinessComponents
{
    /// <summary>
    /// Author:梁雄开
    /// CreatDate:2017-04-05
    /// Description:操作记录
    /// </summary>
    [SugarMapping(TableName = "sc_operation_logs")]
    [CacheSetting(true, PropertyNamesOfArea = "Id")]
    [Serializable]
    public class OperationLogs : IEntity
    {
        #region	构造
        public OperationLogs()
        {

        }
        #endregion

        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static OperationLogs New()
        {
            OperationLogs operationLogs = new OperationLogs();
            operationLogs.Id = 0;
            operationLogs.ApplicationId = 0;
            operationLogs.Source = string.Empty;
            operationLogs.OperationType = string.Empty;
            operationLogs.OperationObjectName = string.Empty;
            operationLogs.OperationObjectId = string.Empty;
            operationLogs.Description = string.Empty;
            operationLogs.OperatorUserid = 0;
            operationLogs.OperatorUsername = string.Empty;
            operationLogs.OperatorIp = string.Empty;
            operationLogs.OperatorUrl = string.Empty;
            operationLogs.DateCreated = DateTime.UtcNow;
            return operationLogs;
        }
        #endregion

        #region	属性
        /// <summary>
        /// 操作记录标识
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 应用Id
        /// </summary>
        public int ApplicationId { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 操作类型标识  （权限项目类型）
        /// </summary>
        [SugarMapping(ColumnName = "operation_type")]
        public string OperationType { get; set; }

        /// <summary>
        /// 操作对象名称     (功能或者菜单)
        /// </summary>
        [SugarMapping(ColumnName = "operation_object_name")]
        public string OperationObjectName { get; set; }

        /// <summary>
        /// 操作对象Id   (功能或者菜单，模块，应用等Id)
        /// </summary>
        //[SugarMapping(ColumnName = "operation_object_id")]
        public string OperationObjectId { get; set; }

        /// <summary>
        /// 操作描述
        /// </summary>
        [SugarMapping(ColumnName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// 操作者UserId
        /// </summary>
        [SugarMapping(ColumnName = "operator_userid")]
        public long OperatorUserid { get; set; }

        /// <summary>
        /// 操作者名称
        /// </summary>
        [SugarMapping(ColumnName = "operator_username")]
        public string OperatorUsername { get; set; }

        /// <summary>
        /// 操作者IP
        /// </summary>
        [SugarMapping(ColumnName = "operator_ip")]
        public string OperatorIp { get; set; }

        /// <summary>
        /// 操作访问的url
        /// </summary>
        [SugarMapping(ColumnName = "operator_url")]
        public string OperatorUrl { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarMapping(ColumnName = "date_created")]
        public DateTime DateCreated { get; set; }

        #endregion

        #region 扩展属性
        public object EntityId { get => Id; }
        #endregion
    }
}
