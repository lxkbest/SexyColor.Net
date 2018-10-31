using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    public class GlobalSettingsSingle
    {

        private static GlobalSettings _instanceGlobalSettings = null;
        private static GlobalSettingsSingle _instance = null;
        private static readonly object lockObject = new object();

        public static GlobalSettingsSingle Instance()
        {
            if (_instance == null)
            {
                lock (lockObject)
                {
                    if (_instance == null)
                    {
                        _instance = new GlobalSettingsSingle();
                    }
                }
            }
            return _instance;
        }

        public void SetGlobalSettings(GlobalSettings settings)
        {
            if (_instanceGlobalSettings == null)
                _instanceGlobalSettings = DIContainer.Resolve<IGlobalSettingsService>().GetGetGlobalSettings();
            _instanceGlobalSettings.GlobalFreeMoneySetting = settings.GlobalFreeMoneySetting;
            _instanceGlobalSettings.GlobalFreightSetting = settings.GlobalFreightSetting;
            _instanceGlobalSettings.GlobalOrderSetting = settings.GlobalOrderSetting;
            _instanceGlobalSettings.GlobalReturnGoodsSetting = settings.GlobalReturnGoodsSetting;
        }

        public GlobalSettings GetGlobalSettings()
        {
            if (_instanceGlobalSettings == null)
                _instanceGlobalSettings = DIContainer.Resolve<IGlobalSettingsService>().GetGetGlobalSettings();
            return _instanceGlobalSettings;
        }
    }
}
