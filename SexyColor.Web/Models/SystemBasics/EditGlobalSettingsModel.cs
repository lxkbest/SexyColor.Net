using SexyColor.BusinessComponents;
using System.ComponentModel.DataAnnotations;

namespace SexyColor.Web
{
    public class EditGlobalSettingsModel
    {
        /// <summary>
        /// 全局运费设置
        /// </summary>
        [Display(Name = "全局运费设置")]
        [Required(ErrorMessage = "{0}为必填选项")]
        public int GlobalFreightSetting { get; set; }

        /// <summary>
        /// 全局退货期限设置
        /// </summary>
        [Display(Name = "全局退货期限设置")]
        [Required(ErrorMessage = "{0}为必填选项")]
        public int GlobalReturnGoodsSetting { get; set; }

        /// <summary>
        /// 全局下单有效时间
        /// </summary>
        [Display(Name = "全局下单有效时间")]
        [Required(ErrorMessage = "{0}为必填选项")]
        public int GlobalOrderSetting { get; set; }

        /// <summary>
        /// 全局免运费达到金额
        /// </summary>
        [Display(Name = "全局免运费达到金额")]
        [Required(ErrorMessage = "{0}为必填选项")]
        public decimal GlobalFreeMoneySetting { get; set; }

        /// <summary>
        /// 将EditGlobalSettingsModel转换成GlobalSettings
        /// </summary>
        /// <returns></returns>
        public GlobalSettings AsGlobalSettings()
        {
            GlobalSettings settins = GlobalSettings.New();
            settins.GlobalFreightSetting = GlobalFreightSetting;
            settins.GlobalReturnGoodsSetting = GlobalReturnGoodsSetting;
            settins.GlobalOrderSetting = GlobalOrderSetting;
            settins.GlobalFreeMoneySetting = GlobalFreeMoneySetting; 
            return settins;
        }
    }
}
