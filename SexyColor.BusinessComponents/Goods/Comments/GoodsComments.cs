using System;
using MySqlSugar;
using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    /// <summary>
    /// Author:梁雄开
    /// CreatDate:2017-04-05
    /// Description:商品评价
    /// </summary>
    [CacheSetting(true)]
    [SugarMapping(TableName = "sc_goods_comments")]
    [Serializable]
    public class GoodsComments : IEntity
    {
        #region	构造
        public GoodsComments()
        {

        }
        #endregion

        #region	方法

        /// <summary>
        /// 新建
        /// </summary>
        public static GoodsComments New()
        {

            GoodsComments goodsComments = new GoodsComments();
            goodsComments.Commentid = 0;
            goodsComments.Goodsid = 0;
            goodsComments.Userid = 0;
            goodsComments.Parentid = 0;
            goodsComments.UserName = string.Empty;
            goodsComments.Body = string.Empty;
            goodsComments.IsPrivate = false;
            goodsComments.Status = false;
            goodsComments.ChildPostCount = 0;
            goodsComments.IsLocked = false;
            goodsComments.Ip = string.Empty;
            goodsComments.DateCreated = DateTime.UtcNow;
            goodsComments.CommentImageUrl1 = string.Empty;
            goodsComments.CommentImageUrl2 = string.Empty;
            goodsComments.CommentImageUrl3 = string.Empty;
            goodsComments.CommentImageUrl4 = string.Empty;
            return goodsComments;
        }
        #endregion

        #region	属性
        /// <summary>
        /// 评论Id
        /// </summary>
        [SugarMapping(ColumnName = "commentid")]
        public long Commentid { get; set; }

        /// <summary>
        /// 商品Id
        /// </summary>
        [SugarMapping(ColumnName = "goodsid")]
        public long Goodsid { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        [SugarMapping(ColumnName = "userid")]
        public long Userid { get; set; }

        /// <summary>
        /// 父评论  0表示父评论，默认0
        /// </summary>
        [SugarMapping(ColumnName = "parentid")]
        public long Parentid { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        [SugarMapping(ColumnName = "user_name")]
        public string UserName { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [SugarMapping(ColumnName = "body")]
        public string Body { get; set; }

        /// <summary>
        /// 是否悄悄评论
        /// </summary>
        [SugarMapping(ColumnName = "is_private")]
        public bool IsPrivate { get; set; }

        /// <summary>
        /// 状态  暂时不清楚有什么用留着
        /// </summary>
        [SugarMapping(ColumnName = "status")]
        public bool Status { get; set; }

        /// <summary>
        /// 子评论数
        /// </summary>
        [SugarMapping(ColumnName = "child_post_count")]
        public int ChildPostCount { get; set; }

        /// <summary>
        /// 是否锁定  0.否，1.是 默认否
        /// </summary>
        [SugarMapping(ColumnName = "is_locked")]
        public bool IsLocked { get; set; }

        /// <summary>
        /// 评论人Ip
        /// </summary>
        [SugarMapping(ColumnName = "ip")]
        public string Ip { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [SugarMapping(ColumnName = "date_created")]
        public System.DateTime DateCreated { get; set; }

        /// <summary>
        /// 评论嗮图1
        /// </summary>
        [SugarMapping(ColumnName = "comment_image_url1")]
        public string CommentImageUrl1 { get; set; }

        /// <summary>
        /// 评论嗮图2
        /// </summary>
        [SugarMapping(ColumnName = "comment_image_url2")]
        public string CommentImageUrl2 { get; set; }

        /// <summary>
        /// 评论嗮图3
        /// </summary>
        [SugarMapping(ColumnName = "comment_image_url3")]
        public string CommentImageUrl3 { get; set; }

        /// <summary>
        /// 评论嗮图4
        /// </summary>
        [SugarMapping(ColumnName = "comment_image_url4")]
        public string CommentImageUrl4 { get; set; }

        /// <summary>
        /// 点赞数
        /// </summary>
        [SugarMapping(ColumnName = "thumbsup_count")]
        public int ThumbsupCount { get; set; }

        /// <summary>
        /// 评分等级
        /// </summary>
        [SugarMapping(ColumnName = "comment_level")]
        public int CommentLevel { get; set; }

        /// <summary>
        /// 评分类型
        /// </summary>
        [SugarMapping(ColumnName = "comment_type")]
        public int CommentType { get; set; }

        /// <summary>
        /// 是否晒图
        /// </summary>
        [SugarMapping(ColumnName = "is_comment_image")]
        public int IsCommentImage { get; set; }

        #endregion

        #region 扩展属性
        public object EntityId { get => Commentid; }
        #endregion
    }
}
