using SexyColor.BusinessComponents;
using System.Collections.Generic;
using System.Linq;

namespace SexyColor.CommonComponents
{
    public static class PermissionsCollction
    {
        private static bool isPass = false;
        private static IDictionary<string, object> permissionItemsCollction;
        public static IDictionary<string, object> PermissionItemsCollction { get => permissionItemsCollction; }
        public static bool IsPass { get => isPass; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="havePermission"></param>
        /// <param name="allPermission"></param>
        /// <param name="isSuperManage"></param>
        public static void InitCollction(IEnumerable<string> havePermission, IEnumerable<PermissionItems> allPermission, bool isSuperManage)
        {
            if (!isSuperManage)
            {
                permissionItemsCollction = new Dictionary<string, object>();
                allPermission.Each(delegate (PermissionItems item)
                {
                    GenerateDictionary(item, havePermission);
                });
            }
            else
            {
                isPass = true;
                permissionItemsCollction = new Dictionary<string, object>();
            }
        }

        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="item"></param>
        /// <param name="havePermission"></param>
        private static void GenerateDictionary(PermissionItems item, IEnumerable<string> havePermission)
        {
            var def = havePermission.FirstOrDefault(w => w == item.Itemkey);
            if (!string.IsNullOrEmpty(def))
            {
                if (item.ItemType == (int)PermissionItemsType.OrdinaryButton)
                {
                    string[] urlArray = item.ItemUrl.Split('/');
                    permissionItemsCollction.Add(urlArray[urlArray.Length - 1], item);
                }
            }
        }

        /// <summary>
        /// 清空
        /// </summary>
        public static void Clear()
        {
            permissionItemsCollction = null;
        }
    }
}
