using MySqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace SexyColor.Infrastructure
{
    /// <summary>
    /// Author:梁雄开
    /// CreatDate:2017-04-05
    /// Description:任务信息
    /// </summary>
    [SugarMapping(TableName = "sc_task_info")]
    [CacheSetting(false)]
    public class TaskInfo : IEntity
    {
        #region	构造
        public TaskInfo()
        {

        }
        #endregion

        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static TaskInfo New()
        {
            TaskInfo taskInfo = new TaskInfo();
            return taskInfo;
        }

        public string GetRulePart(RulePart rulePart)
        {
            if (string.IsNullOrEmpty(this.TaskRule))
            {
                if (RulePart.dayofweek != rulePart)
                {
                    return "1";
                }
                return null;
            }
            string str = this.TaskRule.Split(new char[] { ' ' }).GetValue((int)rulePart).ToString();
            switch (str)
            {
                case "*":
                case "?":
                    if (RulePart.dayofweek != rulePart)
                    {
                        return "1";
                    }
                    return null;
            }
            if (str.Contains("/"))
            {
                return str.Substring(str.IndexOf("/") + 1);
            }
            return str;
        }
        #endregion

        #region	属性
        /// <summary>
        /// 任务队列标识
        /// </summary>
        [SugarMapping(ColumnName = "id")]
        public int Id { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        [SugarMapping(ColumnName = "task_name")]
        public string TaskName { get; set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        [SugarMapping(ColumnName = "task_type")]
        public string TaskType { get; set; }

        /// <summary>
        /// 任务时间规则
        /// </summary>
        [SugarMapping(ColumnName = "task_rule")]
        public string TaskRule { get; set; }

        /// <summary>
        ///  是否启用任务   0=否、1=是
        /// </summary>
        [SugarMapping(ColumnName = "is_enabled")]
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 任务是否正在运行  0=否、1=是
        /// </summary>
        [SugarMapping(ColumnName = "is_runing")]
        public bool IsRuning { get; set; }

        /// <summary>
        /// 上次开始时间
        /// </summary>
        [SugarMapping(ColumnName = "last_start_date")]
        public System.DateTime? LastStartDate { get; set; }

        /// <summary>
        /// 上次结束时间
        /// </summary>
        [SugarMapping(ColumnName = "last_end_date")]
        public System.DateTime? LastEndDate { get; set; }

        /// <summary>
        /// 上次运行是否成功   0=否、1=是
        /// </summary>
        [SugarMapping(ColumnName = "last_success")]
        public bool LastSuccess { get; set; }

        /// <summary>
        /// 下次运行时间
        /// </summary>
        [SugarMapping(ColumnName = "next_date")]
        public System.DateTime? NextDate { get; set; }

        /// <summary>
        /// 任务开始时间
        /// </summary>
        [SugarMapping(ColumnName = "start_date")]
        public System.DateTime StartDate { get; set; }

        /// <summary>
        /// 任务结束时间
        /// </summary>
        [SugarMapping(ColumnName = "end_date")]
        public System.DateTime? EndDate { get; set; }

 

        #endregion

        #region 扩展属性
        object IEntity.EntityId
        {
            get
            {
                return this.Id;
            }
        }
        #endregion
    }
}

