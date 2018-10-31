namespace SexyColor.BusinessComponents
{
    public class PointLogsQuery
    {
        /// <summary>
        /// 用户积分标识
        /// </summary>
        public long Id = 0;

        /// <summary>
        /// 用户Id
        /// </summary>
        public long Userid = 0;

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName = string.Empty;

        /// <summary>
        /// 积分项目标识
        /// </summary>
        public string Itemskey = string.Empty;

        /// <summary>
        /// 积分项目名称
        /// </summary>
        public string Itemsname = string.Empty;

        /// <summary>
        /// 积分值   (概念为：币，豆，丸)
        /// </summary>
        public int? SexyPoints = null;

        /// <summary>
        /// 经验值
        /// </summary>
        public int? ExperiencePoints = null;

        /// <summary>
        /// 是否收入  0.支出，1.收入
        /// </summary>
        public bool? IsIncome = null;

        /// <summary>
        /// 获取时间
        /// </summary>
        public System.DateTime? DateCreated = null;

        /// <summary>
        /// 有效时间
        /// </summary>
        public System.DateTime? DateOverdue = null;

        public PointLogsSortBy? PointLogsSortBy = null;
    }

    /// <summary>
    /// 排序方式
    /// </summary>
    public enum PointLogsSortBy
    {
        /// <summary>
        /// 根据Id排序
        /// </summary>
        Id = 1,

        /// <summary>
        /// 根据Id排序倒序排列
        /// </summary>
        Id_Desc = 2,

        /// <summary>
        /// 根据是否启用排序
        /// </summary>
        IsEnabled = 3,
    }
}
