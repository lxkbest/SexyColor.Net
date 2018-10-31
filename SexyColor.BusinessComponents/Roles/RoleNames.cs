namespace SexyColor.BusinessComponents
{
    public class RoleNames
    {
        private static RoleNames _instance = new RoleNames();
        public static RoleNames Instance()
        {
            return _instance;
        }
        private RoleNames()
        { }

        /// <summary>
        /// 超级管理员
        /// </summary>
        public string SuperAdministrator()
        {
            return "SuperAdministrator";
        }

        /// <summary>
        /// 内容管理员
        /// </summary>
        public string ContentAdministrator()
        {
            return "ContentAdministrator";
        }

        /// <summary>
        /// 圈子管理员
        /// </summary>
        public string BarAdministrator()
        {
            return "ContentAdministrator";
        }

        /// <summary>
        /// 群组管理员
        /// </summary>
        public string GroupAdministrator()
        {
            return "ContentAdministrator";
        }

        

        /// <summary>
        /// 注册用户
        /// </summary>
        public string RegisteredUsers()
        {
            return "RegisteredUsers";
        }

        /// <summary>
        /// 拥有者
        /// </summary>
        public string Owner()
        {
            return "Owner";
        }
    }
}
