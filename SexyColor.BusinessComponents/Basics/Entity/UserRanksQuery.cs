namespace SexyColor.BusinessComponents
{
    public class UserRanksQuery
    {
        /// <summary>
        /// 级别  从1级开始
        /// </summary>
        public string Rank { get; set; }

        /// <summary>
        /// 级别名称
        /// </summary>
        public string RankName { get; set; }

        /// <summary>
        /// 经验值下限    （表示：某个等级最低要求多少经验）
        /// </summary>
        public string PointLower { get; set; }

        /// <summary>
        /// 排序方式
        /// </summary>
        public UserRanksSortBy? UserRanksSortBy = null;
    }

    /// <summary>
    /// 排序方式
    /// </summary>
    public enum UserRanksSortBy
    {
        /// <summary>
        /// 根据Rank排序
        /// </summary>
        Rank = 1,

        /// <summary>
        /// 根据Rank排序倒序排列
        /// </summary>
        Rank_Desc = 2,

        /// <summary>
        /// 根据是否启用排序
        /// </summary>
        IsEnabled = 3,
    }
}
