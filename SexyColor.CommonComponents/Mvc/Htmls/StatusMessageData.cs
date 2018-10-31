using Newtonsoft.Json;

namespace SexyColor.CommonComponents
{
    public sealed class StatusMessageData
    {
        /// <summary>
        /// 信息内容
        /// </summary>
        [JsonProperty("messageContent")]
        public string MessageContent { get; set; }
        /// <summary>
        /// 信息类型
        /// </summary>
        [JsonProperty("messageType")]
        public StatusMessageType MessageType { get; set; }

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="messageType">消息类型</param>
        /// <param name="messageContent">消息内容</param>
        public StatusMessageData(StatusMessageType messageType, string messageContent)
        {
            this.MessageType = messageType;
            this.MessageContent = messageContent;
        }
    }

    /// <summary>
    /// 提示消息类别
    /// </summary>
    public enum StatusMessageType
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 1,

        /// <summary>
        /// 错误
        /// </summary>
        Error = -1,

        /// <summary>
        /// 提示信息
        /// </summary>
        Hint = 0
    }
}
